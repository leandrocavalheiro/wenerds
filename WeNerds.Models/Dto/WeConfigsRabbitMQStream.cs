using System.Net;

namespace WeNerds.Models.Dto;

public readonly record struct WeConfigsRabbitMQStream(string Host, int Port, string UserName, string Password, 
                                                        string VirtualHost = "/", bool LoadBalancer = false, bool SuperStream = false,
                                                        int NumerOfStreams = 4, byte ProducersPerConnection = 2, byte ConsumersPerConnection = 2,
                                                        int Producers = 3, int Consumers = 2, int DelayDuringSendMs = 1, int MessagesPerProducer = 1_000_000, string StreamName = "WeStream")
{
    public bool IsLocalhost()
        => string.IsNullOrWhiteSpace(Host) || Host == IPAddress.Loopback.ToString() || Host.Equals("localhost", StringComparison.OrdinalIgnoreCase);

    public IPEndPoint GetEndPoint()
    {
        if (IsLocalhost())
            return new IPEndPoint(IPAddress.Loopback, Port);
        
            return new IPEndPoint(IPAddress.Parse(Host), Port);
    }
}
