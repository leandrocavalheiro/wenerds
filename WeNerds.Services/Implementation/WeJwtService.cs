using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeNerds.Commons;
using WeNerds.Commons.Extensions;
using WeNerds.Models.Dto;
using WeNerds.Models.Types;
using WeNerds.Models.ViewModels;
using WeNerds.Services.Interfaces;

namespace WeNerds.Services.Implementation;

public class WeJwtService : IWeJwtService
{
    private readonly WeToken _weToken;

    public WeJwtService(WeToken weToken)
    {
        _weToken = weToken;
    }
    public (WeTokenResultViewModel token, string codeError, string exceptionMessage) GenarateJwt(string tenantId, string accountId, string userId, string userEmail = "", ICollection<Claim> claims = null)
    {
        try
        {
            if (string.IsNullOrEmpty(_weToken.Secret))
                return (new WeTokenResultViewModel(), "JWTNotConfigured", null);

            var createdAt = WeMethods.Now();
            var expiresAt = createdAt + TimeSpan.FromHours(_weToken.TokenExpiresInHours);
            var refreshTokenExpiresAt = expiresAt.AddHours(_weToken.RefreshTokenExpiresInHours);
            var token = BuildJwtToken(new JwtSecurityTokenHandler(), GetIdentityClaims($"{accountId}", $"{tenantId}", $"{userId}", userEmail, claims), expiresAt);
            var refreshToken = BuildJwtToken(new JwtSecurityTokenHandler(), GetIdentityClaims($"{accountId}", $"{tenantId}", $"{userId}", null, null), refreshTokenExpiresAt);
            return (new WeTokenResultViewModel(userId, token, "Bearer", expiresAt, new WeRefreshTokenViewModel(refreshToken, refreshTokenExpiresAt)), null, null);
        }
        catch (Exception exception)
        {
            return (new WeTokenResultViewModel(), "InternalError", exception.Message);
        }
    }
    private string BuildJwtToken(JwtSecurityTokenHandler tokenHandler, ClaimsIdentity identityClaims, DateTimeOffset expireDate)
    {
        var securityDescriptor = new SecurityTokenDescriptor
        {
            Subject = identityClaims,
            Issuer = _weToken.Issuer,
            Audience = _weToken.Audience,
            Expires = expireDate.DateTime,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_weToken.Secret)), SecurityAlgorithms.HmacSha256Signature)
        };
        
        return tokenHandler.WriteToken(tokenHandler.CreateToken(securityDescriptor));
    }
    private ClaimsIdentity GetIdentityClaims(string accountId, string tenantId, string userId, string userEmail, ICollection<Claim> claims)
    {
        claims ??= new List<Claim>();
        var identityClaims = new ClaimsIdentity(new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())                
            });

        if (string.IsNullOrEmpty(userEmail) == false)
            identityClaims.AddClaim(new Claim(ClaimTypes.Email, userEmail));

        if (tenantId.IsNullOrEmpty() == false)
            identityClaims.AddClaim(new Claim(WeClaim.TenantId, $"{tenantId}"));

        if (accountId.IsNullOrEmpty() == false)        
            identityClaims.AddClaim(new Claim(WeClaim.AccountId, $"{accountId}"));

        // Add user claims
        identityClaims.AddClaims(claims);

        return identityClaims;
    }       
}
