using Microsoft.Extensions.Configuration;
using System.Globalization;
using WeNerds.Commons;

namespace WeNerds.Services.Extensions;

public static class ConfigurationExtensions
{
    public static string GetWeBabelResourcesPath(this IConfiguration configuration)
        => configuration.GetValue(WeConstants.EV_NAME_BABEL_RESOURCES_PATH, WeConstants.DEFAULT_BABEL_RESOURCES_PATH);

    public static string GetWeBabelResourceName(this IConfiguration configuration)
        => configuration.GetValue(WeConstants.EV_NAME_BABEL_RESOURCE_NAME, WeConstants.DEFAULT_BABEL_RESOURCE_NAME);

    public static string GetWeBabelListLanguages(this IConfiguration configuration)
        => configuration.GetValue(WeConstants.EV_NAME_BABEL_LIST_LANGUAGES, WeConstants.DEFAULT_BABEL_LIST_LANGUAGES);

    public static string GetWeBabelDefaultLanguage(this IConfiguration configuration)
    {
        var language = configuration[WeConstants.EV_NAME_BABEL_DEFAULT_LANGUAGE];
        if (string.IsNullOrWhiteSpace(language))
            language = CultureInfo.CurrentUICulture.Name;

        if (string.IsNullOrWhiteSpace(language))
            language = WeConstants.DEFAULT_BABEL_DEFAULT_LANGUAGE;

        return language;
    }

    public static string GetWeBabelDefaultContext(this IConfiguration configuration)
        => configuration.GetValue(WeConstants.EV_NAME_BABEL_DEFAULT_CONTEXT, "");    
}
