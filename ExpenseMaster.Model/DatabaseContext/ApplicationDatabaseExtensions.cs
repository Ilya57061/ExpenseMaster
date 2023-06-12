using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseMaster.Model.DatabaseContext
{
    public static class ApplicationDatabaseExtensions
    {
        public static void AddApplicationDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDatabaseContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                opt => opt.MigrationsAssembly("ExpenseMaster")));
        }
    }
}
