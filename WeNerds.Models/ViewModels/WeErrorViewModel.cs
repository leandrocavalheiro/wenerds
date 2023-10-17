using System.Text.Json;
using WeNerds.Models.Dto;
namespace WeNerds.Models.ViewModels;
public record WeErrorViewModel
{
    public WeNotification[] Error { get; set; }

    public WeErrorViewModel(IEnumerable<WeNotification> errors)
    {
        Error = errors.ToArray();
    }

    public WeErrorViewModel(WeNotification errors)
    {
        Error = new[] { errors };
    }
    public override string ToString()
        => JsonSerializer.Serialize(this);
}
