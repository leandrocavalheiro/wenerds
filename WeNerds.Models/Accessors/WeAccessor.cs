using WeNerds.Models.Interfaces;

namespace WeNerds.Models.Accessors;

public record WeAccessor : IWeAccessor
{
    public Guid? TenantId { get; set; }
    public Guid? AccountId { get; set; }
    public Guid? UserId { get; set; }
    public string Role { get; set; }
    public string Language { get; set; }
    public string TimeZone { get; set; }
    public WeAccessor()
    {
        Language = "pt-BR";
        TimeZone = "GMT+0";
    }
}
