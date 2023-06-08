using Microsoft.Extensions.Primitives;
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
        private async Task InvokeAsync(HttpContext context)
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
        
        }
    }
}
