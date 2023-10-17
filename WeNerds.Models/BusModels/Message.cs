namespace WeNerds.Models.BusModels;

public abstract class Message
{
    public string MessageType { get; private set; }
    public Guid AggregateId { get; protected set; }
    public DateTimeOffset Timestamp { get; private set; }

    protected Message()
    {
        MessageType = GetType().Name;
        Timestamp = DateTimeOffset.UtcNow;        
    }
}
