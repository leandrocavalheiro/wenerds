using System.Text.Json;

namespace WeNerds.Models.ViewModels;

public record WeEnumViewModel(string Key, string Value, string Description)
{
    public override string ToString()
    => JsonSerializer.Serialize(this);
}
