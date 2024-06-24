namespace WeNerds.Models.Types;

public record struct WeSuccess(Guid Id, Guid? AccountId, DateTimeOffset CreatedAt, DateTimeOffset UpdatedAt, DateTimeOffset? DeletedAt = null);


