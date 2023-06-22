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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
              .GetBytes(configuration.GetSection("Authentication:Secret").Value)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = configuration["Authentication:Issuer"],
                        ValidAudience = configuration["Authentication:Audience"]
                    };
                });
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

            ConfigureDataSeeder(serviceProvider);

            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
        public static void ConfigureDataSeeder(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDatabaseContext>();
                var dataSeeder = new DataSeeder(context);
                dataSeeder.Initialize();
            }
        }
    }
}
