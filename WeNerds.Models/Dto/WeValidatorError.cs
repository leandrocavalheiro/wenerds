using System.Text.Json;

namespace WeNerds.Models.Dto;

public readonly record struct WeValidatorError(string PropertyName, string ErrorMessage, string ErrorCode)
{    
    public override readonly string ToString()
            => JsonSerializer.Serialize(this);
}