using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeNerds.Commons.Enumarators;
using WeNerds.WeRabbitMQStreams.Implementations;
using WeNerds.WeRabbitMQStreams.Interfaces;

namespace WeNerds.WeRabbitMQStreams.Configurations;

public static class WeRabbitMQStreamsConfiguration
{
    public static void AddRabbitMQStreams(this IServiceCollection services, IConfiguration configuration, InjectionTypeEnum injectionType = InjectionTypeEnum.Singleton)
    {
        Register(services, injectionType);
    }

    private static void Register(IServiceCollection services, InjectionTypeEnum injectionType)
    {
        try
        {            
            switch (injectionType)
            {
                case InjectionTypeEnum.Transient:
                    services.AddSingleton<IWeRabbitMQStream, WeRabbitMQStream>();
                    break;

                case InjectionTypeEnum.Scoped:
                    services.AddScoped<IWeRabbitMQStream, WeRabbitMQStream>();
                    break;

                default:
                    services.AddSingleton<IWeRabbitMQStream, WeRabbitMQStream>();
                    break;
            }
        }
        catch
        {
            throw;
        }
    }
}
