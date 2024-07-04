using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Text.Json;
using WeNerds.Commons.Extensions;
using WeNerds.Services.Interfaces;
using WeNerds.Services.Models;

namespace WeNerds.Services.Implementation;

public class WeBabel : IWeBabel
{
    private Dictionary<string, string> _localizations;
    private string _context;

    private readonly WeBabelOptions _options;

    public WeBabel(IOptions<WeBabelOptions> options)
    {
        _localizations = LoadLocalizations();
        _context = options.Value.DefaultContext;
        _options = options.Value;
    }

    private Dictionary<string, string> LoadLocalizations()
    {
        if (_options is null)
            return new Dictionary<string, string>();

        var culture = _options.DefaultLanguage;
        var filePath = Path.Combine(_options.ResourcesPath, $"{_options.ResourceName}.{culture}.json");

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Localization file not found: {filePath}");

        if (string.IsNullOrWhiteSpace(_context))
            return LoadWithoutContext(filePath);

        return LoadWithContext(filePath);
    }

    private Dictionary<string, string> LoadWithContext(string filePath)
    {
        var jsonDocument = JsonDocument.Parse(File.ReadAllText(filePath));
        if (jsonDocument is null)
            return new();


        if (!jsonDocument.RootElement.TryGetProperty(_context, out JsonElement contextResult))
            return new();

        var result = JsonSerializer.Deserialize<Dictionary<string, string>>(contextResult.GetRawText());
        _localizations = result;
        return result;
    }
    private Dictionary<string, string> LoadWithoutContext(string filePath)
    {
        var json = File.ReadAllText(filePath);
        if (string.IsNullOrWhiteSpace(json))
            return new();

        var result = json.Deserialize<Dictionary<string, string>>();        
        _localizations = result;
        return result;
    }

    public LocalizedString this[string name]
    {
        get
        {
            var value = _localizations.ContainsKey(name) ? _localizations[name] : name;
            return new LocalizedString(name, value);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var value = _localizations.ContainsKey(name) ? string.Format(_localizations[name], arguments) : name;
            return new LocalizedString(name, value);
        }
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        => _localizations.Select(l => new LocalizedString(l.Key, l.Value)).ToList();

    public WeBabel SetContext(string context)
    {
        _context = context;
        _ = LoadLocalizations();
        return this;
    }

}