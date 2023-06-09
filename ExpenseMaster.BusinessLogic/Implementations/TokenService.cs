﻿using ExpenseMaster.BusinessLogic.Interfaces;
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
            var authenticationSection = _configuration.GetSection("Authentication");
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSection["Secret"]));
        }

        public JwtSecurityToken GenerateJwtToken(User user)
        {
            var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Name, user.Login),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
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

        public int GetUserIdFromClaims(ClaimsPrincipal claimsPrincipal)
        {
            var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);
            
            return userId;
        }
    }
}