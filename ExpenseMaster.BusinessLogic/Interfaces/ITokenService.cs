using ExpenseMaster.DAL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateJwtToken(User user);
        string GetToken(User user);
        int GetUserIdFromClaims(ClaimsPrincipal claimsPrincipal);
    }
}
