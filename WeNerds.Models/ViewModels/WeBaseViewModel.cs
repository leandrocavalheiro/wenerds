namespace WeNerds.Models.ViewModels;

public abstract class WeBaseViewModel
{
    public Guid Id { get; set; }
    public Guid? AccountId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset? DeletadAt { get; set; }
}

