using ExpenseMaster.Model.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMaster.Configuration
{
    public static class ConfigurationService
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDatabaseContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                opt => opt.MigrationsAssembly("ExpenseMaster")));
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }
        public static void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
