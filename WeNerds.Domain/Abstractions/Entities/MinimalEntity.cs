using System.ComponentModel.DataAnnotations.Schema;
using WeNerds.Commons;
using WeNerds.Commons.Extensions;
using WeNerds.Models.Dto;

namespace WeNerds.Domain.Abstractions.Entities;

public abstract class MinimalEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    [NotMapped]
    public ICollection<WeNotification> Notifications { get; set; }

    /// <summary>
    /// Returns whether the two entities are equal. The comparison is made by Id's
    /// </summary>
    /// <returns>A boolean value</returns>
    public bool IsEqual(MinimalEntity entity, MinimalEntity otherEntity)
    {        
        if (entity == null && otherEntity == null) 
            return true;

        if (entity == null || otherEntity == null)
            return false;

        return entity.Id == otherEntity.Id;
    }

    /// <summary>
    /// Returns whether the record is deleted
    /// </summary>
    /// <returns>A boolean value.</returns>
    public bool IsDeleted()
        => DeletedAt != null;
    public bool HasNotification()
        => Notifications.Any();
   
    protected MinimalEntity()
        => Initialize(Guid.NewGuid(), isUtcTime: true);
    protected MinimalEntity(bool isUtcTime)
        => Initialize(Guid.NewGuid(), isUtcTime: isUtcTime);   
    protected MinimalEntity(Guid id)
        => Initialize(id);
    protected MinimalEntity(Guid id, DateTimeOffset createdAt, bool isUtcTime = true)
        => Initialize(id, createdAt, isUtcTime);
    private void Initialize(Guid id, DateTimeOffset? createdAt = null, bool isUtcTime = true)
    {       
        Id = id;
        CreatedAt = createdAt.IsNullOrEmpty() ? WeMethods.Now(isUtcTime) : createdAt.ToDate(isUtcTime);
        UpdatedAt = CreatedAt;
    }
}
