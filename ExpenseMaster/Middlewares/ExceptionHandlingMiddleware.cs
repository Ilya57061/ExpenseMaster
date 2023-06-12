using Microsoft.EntityFrameworkCore;

namespace ExpenseMaster.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DbUpdateException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Error when accessing the database.");
            }
            catch (ArgumentException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Error in query arguments.");
            }
            catch (UnauthorizedAccessException)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Access denied.");
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("There was an error.");
            }
        }
    }
}
