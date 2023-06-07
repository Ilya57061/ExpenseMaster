using Microsoft.EntityFrameworkCore;

namespace ExpenseMaster.Model.DatabaseContext
{
    public class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
