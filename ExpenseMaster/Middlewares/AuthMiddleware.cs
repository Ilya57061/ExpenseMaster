using ExpenseMaster.BusinessLogic.Interfaces;

namespace ExpenseMaster.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;

        public AuthMiddleware(RequestDelegate next, ITokenService authService)
        {
            _next = next;
            _tokenService = authService;
        }

        public async Task Invoke(HttpContext context)
        {

            string authorizationHeader = context.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                await _next(context);
                return;
            }

            string jwtToken = authorizationHeader.ToString().Substring(7);

            string? userLogin = _tokenService.GetUserIdFromToken(jwtToken);
            if (string.IsNullOrEmpty(userLogin))
            {
                context.Response.StatusCode = 401;
                return;
            }
            context.Items["UserLogin"] = userLogin;
            await _next(context);
        }
    }
}
