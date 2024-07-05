using Microsoft.Extensions.DependencyInjection;
using WeNerds.Filters.Implementations;

namespace WeNerds.Filters.Configurations;

public static class WeResultHandlerFilterConfigurations
{
    public static void AddWeResultHandlerFilter(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        switch (serviceLifetime)
        {
            case ServiceLifetime.Transient:
                services.AddTransient<WeResultHandlerFilter>();
                break;
            case ServiceLifetime.Singleton:
                services.AddSingleton<WeResultHandlerFilter>();
                break;
            default:
                services.AddScoped<WeResultHandlerFilter>();
                break;
        }
    }
}
