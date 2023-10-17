using System.Text.Json;

namespace WeNerds.Models.Dto;

public readonly record struct WeNotification(string Field, string MessageCode, string Message, int? Status = null, string InnerException = null, string StackTrace = null)
{
    public override readonly string ToString()
        => JsonSerializer.Serialize(this);
}  