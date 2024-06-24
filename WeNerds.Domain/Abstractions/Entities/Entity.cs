namespace WeNerds.Domain.Abstractions.Entities;

public abstract class Entity : MinimalEntity
{
    public Guid AccountId { get; set; }
    protected Entity() 
    { 
    }
    protected Entity(Guid accountId) : base()
    {
        AccountId = accountId;

    }
    protected Entity(Guid accountId, bool isUtcTime) : base(isUtcTime)
    {
        AccountId = accountId;
    }
    protected Entity(Guid id, Guid accountId) : base(id)
    {
        AccountId = accountId;
    }
    protected Entity(Guid id, Guid accountId, DateTimeOffset createdAt, bool isUtcTime = true) : base(id, createdAt, isUtcTime)
    {
        AccountId = accountId;
    }
}