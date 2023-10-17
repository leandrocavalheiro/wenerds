namespace WeNerds.Models.ViewModels;

public record WeRefreshTokenViewModel(string RefreshToken = "", DateTimeOffset? ExpiresAt = null)
{
}
