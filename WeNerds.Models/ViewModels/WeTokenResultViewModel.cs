using System.Text.Json;

namespace WeNerds.Models.ViewModels;
public record WeTokenResultViewModel(string UserId = null, string AccessToken = null, string TokenType = null, DateTimeOffset? ExpiresAt = null, WeRefreshTokenViewModel RefreshToken = null)
{
    public override string ToString()
            => JsonSerializer.Serialize(this);
}