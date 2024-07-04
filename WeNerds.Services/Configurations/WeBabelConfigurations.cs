using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using WeNerds.Services.Extensions;
using WeNerds.Services.Implementation;
using WeNerds.Services.Interfaces;
using WeNerds.Services.Models;

namespace WeNerds.Services.Configurations;

public static class WeBabelConfigurations
{
    public static void AddWeBabel(this IServiceCollection services, IConfiguration configuration, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {

        var resourcePath = configuration.GetWeBabelResourcesPath();                
        var defaultLanguage = configuration.GetWeBabelDefaultLanguage();

        services.AddLocalization(options => options.ResourcesPath = resourcePath);        
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedLanguages = GetLanguages(configuration);

            options.DefaultRequestCulture = new RequestCulture(defaultLanguage);
            options.SupportedCultures = supportedLanguages;
            options.SupportedUICultures = supportedLanguages;
        });


        services.Configure<WeBabelOptions>(options =>
        {
            options.ResourcesPath = configuration.GetWeBabelResourcesPath();
            options.ResourceName = configuration.GetWeBabelResourceName();
            options.Languages = configuration.GetWeBabelListLanguages();
            options.DefaultLanguage = configuration.GetWeBabelDefaultLanguage();
            options.DefaultContext = configuration.GetWeBabelDefaultContext();
        });


        switch (serviceLifetime)
        {
            case ServiceLifetime.Transient:
                services.AddTransient<IWeBabel, WeBabel>();
                break;
            case ServiceLifetime.Singleton:
                services.AddSingleton<IWeBabel, WeBabel>();
                break;
            default:
                services.AddScoped<IWeBabel, WeBabel>();
                break;
        }        
    }



    public static void UseWeBabel(this IApplicationBuilder app, IConfiguration configuration)
    {

        var supportedLanguages = GetLanguages(configuration);
        var defaultLanguage = configuration.GetWeBabelDefaultLanguage();
        
        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(defaultLanguage),
            SupportedCultures = supportedLanguages,
            SupportedUICultures = supportedLanguages
        };

        app.UseRequestLocalization(localizationOptions);
    }

    private static CultureInfo[] GetLanguages(IConfiguration configuration)
    {
        var listLanguages = configuration.GetWeBabelListLanguages();
        var defaultLanguage = configuration.GetWeBabelDefaultLanguage();
        CultureInfo[] supportedLanguages = [];

        var configuredLanguages = listLanguages.Split(",");

        foreach (var language in configuredLanguages)
            supportedLanguages.Append(new CultureInfo(language));

        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(defaultLanguage),
            SupportedCultures = supportedLanguages,
            SupportedUICultures = supportedLanguages
        };

        return supportedLanguages;
    }

}
