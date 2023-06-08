using ExpenseMaster.BusinessLogic.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ExpenseMaster.Model.Models;
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
            new Claim(JwtRegisteredClaimNames.NameId, user.Login)
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
        public string GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Authentication:Secret"]);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Authentication:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Authentication:Audience"],
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userLogin = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value;

            return userLogin;
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