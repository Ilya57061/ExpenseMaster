using ExpenseMaster.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMaster.Model.DatabaseContext
{
    public class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<FinancialGoal> FinancialGoal { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
