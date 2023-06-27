using Microsoft.EntityFrameworkCore;

namespace ExpenseMaster.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error when accessing the database.");
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Error when accessing the database.");
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error in query arguments.");
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Error in query arguments.");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Access denied.");
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Access denied.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error.");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("There was an error.");
            }
        }
    }
}
