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

    private string _resourcePath;
    private string _resourceName;
    private string _resourceFullPath;
    private string _culture;
    
    public WeBabel(IOptions<WeBabelOptions> options)
    {
        _localizations = LoadLocalizations();
        _context = options.Value.DefaultContext;
        _options = options.Value;
       
        SetFullPath(_resourcePath, _resourceName, _options.DefaultLanguage);
    }

    private void SetFullPath(string resourcePath, string resourceName, string culture = null)
    { 
        _resourcePath = resourcePath;
        _resourceName = resourceName;
        if (!string.IsNullOrWhiteSpace(culture))
            _culture = culture;

        _resourceFullPath = Path.Combine(_resourcePath, $"{_resourceName}.{_culture}.json");
    }
    
    private Dictionary<string, string> LoadLocalizations()
    {
        if (_options is null)
            return [];

        if (!File.Exists(_resourceFullPath))
            throw new FileNotFoundException($"Localization file not found: {_resourceFullPath}");

        if (string.IsNullOrWhiteSpace(_context))
            return LoadWithoutContext();

        return LoadWithContext();
    }

    private Dictionary<string, string> LoadWithContext()
    {
        var jsonDocument = JsonDocument.Parse(File.ReadAllText(_resourceFullPath));
        if (jsonDocument is null)
            return [];


        if (!jsonDocument.RootElement.TryGetProperty(_context, out JsonElement contextResult))
            return [];

        var result = JsonSerializer.Deserialize<Dictionary<string, string>>(contextResult.GetRawText());
        _localizations = result;
        return result;
    }
    private Dictionary<string, string> LoadWithoutContext()
    {
        var json = File.ReadAllText(_resourceFullPath);
        if (string.IsNullOrWhiteSpace(json))
            return [];

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

    public WeBabel SetFileResource(string path, string name, string context = null, string culture = null)
    {
        SetFullPath(path, name, culture);        
        return SetContext(context);        
    }

    public WeBabel SetContext(string context)
    {
        _context = context;
        _ = LoadLocalizations();
        return this;
    }

}