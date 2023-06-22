using ExpenseMaster.DAL.DatabaseContext;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.DAL.Seed
{
    public class DataSeeder
    {
        private readonly ApplicationDatabaseContext _context;
        public DataSeeder(ApplicationDatabaseContext context)
        {
            _context = context;
        }
        public void Initialize()
        {
            SeedCategories();
            SeedExpenses();
            SeedIncomes();
            _context.SaveChanges();
        }

        public void SeedCategories()
        {
            if (!_context.Category.Any())
            {
                var category = new List<Category>()
                {
                    new Category { Name = "Category 1" },
                    new Category { Name = "Category 2" },
                    new Category { Name = "Category 3" },
                    new Category { Name = "Category 4" },
                    new Category { Name = "Category 5" }
                };
                _context.Category.AddRange(category);
            }
        }

        public void SeedExpenses()
        {
            if (!_context.Expenses.Any())
            {
                var expenses = new List<Expense>()
                {
                    new Expense { UserId = 1, CategoryId = 16, Amount = 100.00m, Date = DateTime.Now },
                    new Expense { UserId = 1, CategoryId = 17, Amount = 50.00m, Date = DateTime.Now },
                    new Expense { UserId = 2, CategoryId = 18, Amount = 75.00m, Date = DateTime.Now },
                    new Expense { UserId = 2, CategoryId = 19, Amount = 120.00m, Date = DateTime.Now },
                    new Expense { UserId = 3, CategoryId = 20, Amount = 200.00m, Date = DateTime.Now }
                };
                _context.Expenses.AddRange(expenses);
            }
        }

        public void SeedIncomes()
        {
            if (!_context.Incomes.Any())
            {
                var incomes = new List<Income>()
                {
                    new Income { UserId = 1, CategoryId = 16, Amount = 1000.00m, Date = DateTime.Now },
                    new Income { UserId = 1, CategoryId = 17, Amount = 750.00m, Date = DateTime.Now },
                    new Income { UserId = 2, CategoryId = 18, Amount = 500.00m, Date = DateTime.Now },
                    new Income { UserId = 2, CategoryId = 19, Amount = 1200.00m, Date = DateTime.Now },
                    new Income { UserId = 3, CategoryId = 20, Amount = 800.00m, Date = DateTime.Now }
                };
                _context.Incomes.AddRange(incomes);
            }
        }
    }
}
