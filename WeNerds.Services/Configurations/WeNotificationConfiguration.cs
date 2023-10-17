using Microsoft.Extensions.DependencyInjection;
using WeNerds.Commons.Enumarators;
using WeNerds.Services.Implementation;
using WeNerds.Services.Interfaces;

namespace WeNerds.Services.Configurations;

public static class WeNotificationConfiguration
{
    public static void AddWeNotification(this IServiceCollection services, InjectionTypeEnum injectionType = InjectionTypeEnum.Scoped)
    {
        switch (injectionType)
        {
            case InjectionTypeEnum.Scoped:
                services.AddScoped<IWeNotificationService, WeNotificationService>();
                break;
            case InjectionTypeEnum.Singleton:
                services.AddSingleton<IWeNotificationService, WeNotificationService>();                
                break;
            default:
                services.AddTransient<IWeNotificationService, WeNotificationService>();                
                break;
        }
        
    }
}
