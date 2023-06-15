using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ExpenseMaster.Middlewares
{
    public class JwtTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtTokenMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("Authorization", out StringValues authHeader))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Authorization header not found");

                return;
            }

            if (!authHeader.ToString().StartsWith("Bearer "))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid authorization header format");

                return;
            }
            string token = authHeader.ToString().Substring(7);

            try
            {

                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Secret"])),
                    ValidateAudience = false
                };
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                context.User = principal;

                await _next(context);
            }
            catch (Exception)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid token");

                return;
            }
        }
    }
}
