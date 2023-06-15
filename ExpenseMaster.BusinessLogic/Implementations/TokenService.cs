using ExpenseMaster.BusinessLogic.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ExpenseMaster.DAL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Secret"]));
        }

        public JwtSecurityToken GenerateJwtToken(User user)
        {
            var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.NameId, user.Login),
            new Claim(ClaimTypes.Role, user.Role.Name.ToString()),
            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new JwtSecurityToken(
                 issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentication:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Authentication:TokenLifetimeMinutes"])),
                signingCredentials: creds,
                notBefore: DateTime.UtcNow);

            return tokenDescriptor;
        }

        public string GetToken(User user)
        {
            var jwtToken = GenerateJwtToken(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedJwt = tokenHandler.WriteToken(jwtToken);

            return encodedJwt;
        }
    }
}