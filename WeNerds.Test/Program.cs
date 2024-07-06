using WeNerds.Services.Configurations;
using WeNerds.Test;
using WeNerds.WeRabbitMQStreams.Configurations;

string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .Build();


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddWeJwt(config);
        services.AddWeBabel(config);
        services.AddRabbitMQStreams(config);
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
