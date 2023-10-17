using System.Text.Json;

namespace WeNerds.Models.Dto;

public record WeToken(string Secret, uint TokenExpiresInHours, uint RefreshTokenExpiresInHours, string Issuer, string Audience)
{    
    public override string ToString()
            => JsonSerializer.Serialize(this);
}