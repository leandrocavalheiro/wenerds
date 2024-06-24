
using Microsoft.Extensions.Configuration;
using RabbitMQ.Stream.Client;
using RabbitMQ.Stream.Client.Reliable;
using System.Net;
using WeNerds.Commons.Extensions;
using WeNerds.Models.Dto;
using WeNerds.WeRabbitMQStreams.Extensions;
using WeNerds.WeRabbitMQStreams.Interfaces;

namespace WeNerds.WeRabbitMQStreams.Implementations;

public class WeRabbitMQStream : IWeRabbitMQStream
{
    private readonly IConfiguration _configuration;
    private readonly WeConfigsRabbitMQStream _config;
    public WeRabbitMQStream(IConfiguration configuration)
    {
        _configuration = configuration;
        _config = ReadConfigurations();        
    }


    #region Public Methods
    public async Task<StreamSystem> CreateNewStreamSystemAsync()
    {
        var newStream = ConfigurNewStream();
        return await StreamSystem.Create(newStream).ConfigureAwait(false);
    }
    public async Task CloseStreamSystemAsync(StreamSystem streamSystem)
        => await streamSystem.Close().ConfigureAwait(false);

    public async Task<Producer> CreateNewProducerAsync(string streamName, StreamSystem streamSystem = null)
    {

        if (streamSystem == null)
            streamSystem = await CreateNewStreamSystemAsync();

        await CreateNewStreamAsync(streamSystem, streamName);
        
        var config = new ProducerConfig(streamSystem is null ? await CreateNewStreamSystemAsync() : streamSystem, streamName);
        return  await Producer.Create(config,  null).ConfigureAwait(false);
    }
    
    public async Task<Dictionary<Guid, bool>> PublishWithTasks(Dictionary<string, object> messages, CancellationToken cancellationToken)
    {
        ICollection<Producer> producerList = new List<Producer>();
        var threads = new Thread[messages.Count];

        var currentMessage = 0;
        var result = new Dictionary<Guid, bool>();
        foreach (var message in messages)
        {
            threads[currentMessage] = new Thread(async () =>
            {
                var resultThread = await PublishAsync(message.Key, message.Value);
               // result.FirstOrDefault(p => p.Key == resultThread.ToGuid()).Value = true;

            });




            //var result = Task.Run(async () => await PublishAsync(message.Key, message.Value));
            //var thread = new Thread(async () => await PublishAsync(message.Key, message.Value));        
        }

        return result;
        




    }
    private async Task<Guid?> PublishAsync(string stream, object message)
    {
        if (message == null)
            return null;
        var producer = await CreateNewProducerAsync(stream);
        return await producer.PublishAsync(message.Serialize());
    }
    #endregion

    private async Task CreateNewStreamAsync(StreamSystem streamSystem, string streamName, bool forceRecreate = false)
    {
        if (await streamSystem.StreamExists(streamName))
        {
            if (!forceRecreate)
                return;

            await streamSystem.DeleteStream(streamName);
        }
        
        await streamSystem.CreateStream(new StreamSpec(streamName) { MaxLengthBytes = 1_073_741_824, })
                            .ConfigureAwait(false);
    }
    private WeConfigsRabbitMQStream ReadConfigurations()
    {
        var virtualHost = _configuration.GetValue<string>("RMQ_STREAM_VIRTUAL_HOST", "/");
        var host = _configuration.GetValue<string>("RMQ_STREAM_HOST", "localhost");
        var port = _configuration.GetValue<int>("RMQ_STREAM_PORT");
        var user = _configuration.GetValue<string>("RMQ_STREAM_USER");
        var password = _configuration.GetValue<string>("RMQ_STREAM_PASSWORD");
        var producersPerConnection = _configuration.GetValue<byte>("RMQ_STREAM_PRODUCERS_PER_CONNECTION");
        var consumersPerConnection = _configuration.GetValue<byte>("RMQ_STREAM_CONSUMERS_PER_CONNECTION");

        return new WeConfigsRabbitMQStream(host, port, user, password, virtualHost, ProducersPerConnection: producersPerConnection, ConsumersPerConnection: consumersPerConnection);
    }

    private StreamSystemConfig ConfigurNewStream()    
        => new StreamSystemConfig()
        {
            UserName = _config.UserName,
            Password = _config.Password,
            Endpoints = new List<EndPoint>() { _config.GetEndPoint() },
            ConnectionPoolConfig = new ConnectionPoolConfig()
            {
                ProducersPerConnection = _config.ProducersPerConnection,
                ConsumersPerConnection = _config.ConsumersPerConnection,
            },
            VirtualHost = _config.VirtualHost,
        };        
}
