using Microsoft.Extensions.DependencyInjection;
using WeNerds.Notification.Implementation;
using WeNerds.Notification.Interfaces;

namespace WeNerds.Notification.Configurations;

public static class NotificationConfiguration
{
    public static void AddWeNerdsNotification(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {

        switch (lifetime)
        {
            case ServiceLifetime.Transient:
                services.AddTransient<INotificationService, NotificationService>();
                break;
            case ServiceLifetime.Singleton:
                services.AddSingleton<INotificationService, NotificationService>();                
                break;
            default:
                services.AddScoped<INotificationService, NotificationService>();                
                break;
        }        
    }
}
