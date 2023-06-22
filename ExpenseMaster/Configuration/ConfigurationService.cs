using ExpenseMaster.Middlewares;
using ExpenseMaster.DAL.DatabaseContext;
using ExpenseMaster.DAL.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ExpenseMaster.Configuration
{
    public static class ConfigurationService
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDatabase(configuration);
            services.AddControllers();
            services.AddCustomServices();
            services.AddCustomAutoMapper();
            services.AddEndpointsApiExplorer();
            services.AddCustomSwagger();
            services.AddCustomLogging();
            services.AddCustomAuthentication(configuration);
        }

        public static void Configure(WebApplication app, IServiceProvider serviceProvider)
        {
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.MapControllers();
            app.ConfigureDataSeeder();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
