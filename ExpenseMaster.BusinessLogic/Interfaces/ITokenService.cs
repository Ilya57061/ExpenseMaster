using ExpenseMaster.Model;
using System.IdentityModel.Tokens.Jwt;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateJwtToken(User user);
        string GetUserIdFromToken(string token);
        string GetToken(User user);
    }
}
