using Microsoft.Extensions.DependencyInjection;
using WeNerds.Services.Implementation;
using WeNerds.Services.Interfaces;

namespace WeNerds.Services.Configurations;

public static class WeCriptographyConfiguration
{
    public static void AddWeCriptography(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        switch (serviceLifetime)
        {
            case ServiceLifetime.Transient:
                services.AddTransient<IWeCriptography, WeCriptography>();                
                break;
            case ServiceLifetime.Singleton:
                services.AddSingleton<IWeCriptography, WeCriptography>();                
                break;
            default:
                services.AddScoped<IWeCriptography, WeCriptography>();                
                break;
        }        
    }
}
