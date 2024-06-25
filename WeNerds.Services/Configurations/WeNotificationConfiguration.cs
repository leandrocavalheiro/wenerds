using Microsoft.Extensions.DependencyInjection;
using WeNerds.Services.Implementation;
using WeNerds.Services.Interfaces;

namespace WeNerds.Services.Configurations;

public static class WeNotificationConfiguration
{
    public static void AddWeNotification(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        switch (serviceLifetime)
        {
            case ServiceLifetime.Transient:
                services.AddTransient<IWeNotificationService, WeNotificationService>();                
                break;
            case ServiceLifetime.Singleton:
                services.AddSingleton<IWeNotificationService, WeNotificationService>();                
                break;
            default:
                services.AddScoped<IWeNotificationService, WeNotificationService>();                
                break;
        }
        
    }
}
