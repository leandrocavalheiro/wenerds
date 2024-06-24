using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeNerds.Models.Extensions;

public static class ObjectExtensions
{
    public static string Serialize(this object value, bool formatted = false)
    {
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = formatted,

        };
        return JsonSerializer.Serialize(value, options);
    }
}
