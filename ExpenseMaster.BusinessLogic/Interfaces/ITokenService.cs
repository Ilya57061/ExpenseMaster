using ExpenseMaster.DAL.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateJwtToken(User user);
        string GetToken(User user);
    }
}
