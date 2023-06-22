using ExpenseMaster.DAL.DatabaseContext;
using ExpenseMaster.DAL.Seed;

namespace ExpenseMaster.Configuration
{
    public static class DataSeederExtensions
    {
        public static void ConfigureDataSeeder(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<ApplicationDatabaseContext>();
                var dataSeeder = new DataSeeder(context);
                dataSeeder.Initialize();
            }
        }
    }
}
