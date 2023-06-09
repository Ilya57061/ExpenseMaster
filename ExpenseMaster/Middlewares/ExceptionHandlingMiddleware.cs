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
                await context.Response.WriteAsync("Ошибка при обращении к базе данных.");
            }
            catch (ArgumentException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Ошибка в аргументах запроса.");
            }
            catch (UnauthorizedAccessException)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Отказано в доступе.");
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Произошла ошибка.");
            }
        }
    }
}
