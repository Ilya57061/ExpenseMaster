using ExpenseMaster.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMaster.DAL.Seed
{
    public static class SeedData
    {
        public static void Initialize(ModelBuilder modelBuilder)
        {
            SeedCategories(modelBuilder);
            SeedExpenses(modelBuilder);
            SeedIncomes(modelBuilder);
        }

        private static void SeedCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Category 1" },
                new Category { Id = 2, Name = "Category 2" },
                new Category { Id = 3, Name = "Category 3" },
                new Category { Id = 4, Name = "Category 4" },
                new Category { Id = 5, Name = "Category 5" }
            );
        }

        private static void SeedExpenses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>().HasData(
                new Expense { Id = 1, UserId = 1, CategoryId = 1, Amount = 100.00m, Date = DateTime.Now },
                new Expense { Id = 2, UserId = 1, CategoryId = 2, Amount = 50.00m, Date = DateTime.Now },
                new Expense { Id = 3, UserId = 2, CategoryId = 1, Amount = 75.00m, Date = DateTime.Now },
                new Expense { Id = 4, UserId = 2, CategoryId = 3, Amount = 120.00m, Date = DateTime.Now },
                new Expense { Id = 5, UserId = 3, CategoryId = 2, Amount = 200.00m, Date = DateTime.Now }
            );
        }

        private static void SeedIncomes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Income>().HasData(
                new Income { Id = 1, UserId = 1, CategoryId = 1, Amount = 1000.00m, Date = DateTime.Now },
                new Income { Id = 2, UserId = 1, CategoryId = 2, Amount = 750.00m, Date = DateTime.Now },
                new Income { Id = 3, UserId = 2, CategoryId = 1, Amount = 500.00m, Date = DateTime.Now },
                new Income { Id = 4, UserId = 2, CategoryId = 3, Amount = 1200.00m, Date = DateTime.Now },
                new Income { Id = 5, UserId = 3, CategoryId = 2, Amount = 800.00m, Date = DateTime.Now }
            );
        }
    }
}
