using RabbitMQ.Stream.Client;
using RabbitMQ.Stream.Client.Reliable;

namespace WeNerds.WeRabbitMQStreams.Interfaces;

public interface IWeRabbitMQStream
{
    Task<Producer> CreateNewProducerAsync(string streamName, StreamSystem streamSystem = null);    
}
