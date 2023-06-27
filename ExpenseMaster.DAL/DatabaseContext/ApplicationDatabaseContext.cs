using ExpenseMaster.DAL.Constants;
using ExpenseMaster.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ExpenseMaster.DAL.DatabaseContext
{
    public class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext()
        {
            
        }
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Role>().HasData(
                new Role[] { new Role { Id=1, Name=RoleType.User},
                    new Role {Id=2, Name=RoleType.Admin}});
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<FinancialGoal> FinancialGoal { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
