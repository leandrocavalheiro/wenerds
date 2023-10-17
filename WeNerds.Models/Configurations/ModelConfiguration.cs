using Microsoft.Extensions.DependencyInjection;
using WeNerds.Models.Accessors;
using WeNerds.Models.Interfaces;

namespace WeNerds.Models.Configurations;

public static class ModelConfiguration
{
    public static void AddWeModels(this IServiceCollection services)
    {        
        services.AddScoped<IWeAccessor, WeAccessor>();
    }
}
