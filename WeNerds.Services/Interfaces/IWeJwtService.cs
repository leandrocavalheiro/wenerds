using System.Security.Claims;
using WeNerds.Models.ViewModels;

namespace WeNerds.Services.Interfaces;

public interface IWeJwtService
{
    (WeTokenResultViewModel token, string codeError, string exceptionMessage) GenarateJwt(string accountId, string tenantId, string userId, string userEmail = "", ICollection<Claim> claims = null);
}
