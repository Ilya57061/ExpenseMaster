using ExpenseMaster.DAL.DatabaseContext;
using ExpenseMaster.DAL.Models;
using ExpenseMaster.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ExpenseMaster.Tests.Repository
{
    public class BudgetRepositoryTests
    {
        [Fact]
        public async Task GetByIdAsync_ReturnsBudget()
        {
            var budgets = new List<Budget>
            {
                new Budget { Id = 1, UserId = 1 },
                new Budget { Id = 2, UserId = 1 },
                new Budget { Id = 3, UserId = 2 }
            };
            var mockDbSet = GetMockDbSet(budgets);
            var mockContext = new Mock<ApplicationDatabaseContext>();
            mockContext.Setup(c => c.Set<Budget>()).Returns(mockDbSet.Object);

            var repository = new BudgetRepository(mockContext.Object);

            var result = await repository.GetByIdAsync(2);

            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
        }

        [Fact]
        public async Task GetBudgetsByUserIdAsync_ReturnsBudgets()
        {
            var budgets = new List<Budget>
            {
                new Budget { Id = 1, UserId = 1 },
                new Budget { Id = 2, UserId = 1 },
                new Budget { Id = 3, UserId = 2 }
            };
            var mockDbSet = GetMockDbSet(budgets);
            var mockContext = new Mock<ApplicationDatabaseContext>();
            mockContext.Setup(c => c.Set<Budget>()).Returns(mockDbSet.Object);

            var repository = new BudgetRepository(mockContext.Object);

            var result = await repository.GetBudgetsByUserIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetBudgetByCategoryIdAsync_ReturnsBudget()
        {
            var budgets = new List<Budget>
            {
                new Budget { Id = 1, UserId = 1, CategoryId = 1 },
                new Budget { Id = 2, UserId = 1, CategoryId = 2 },
                new Budget { Id = 3, UserId = 2, CategoryId = 1 }
            };
            var mockDbSet = GetMockDbSet(budgets);
            var mockContext = new Mock<ApplicationDatabaseContext>();
            mockContext.Setup(c => c.Set<Budget>()).Returns(mockDbSet.Object);

            var repository = new BudgetRepository(mockContext.Object);

            var result = await repository.GetBudgetByCategoryIdAsync(1, 2);

            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
        }

        [Fact]
        public async Task GetBudgetsExceedingThresholdAsync_ReturnsBudgets()
        {
            var budgets = new List<Budget>
            {
                new Budget { Id = 1, UserId = 1, WarningThreshold = 100, Limit = 200 },
                new Budget { Id = 2, UserId = 1, WarningThreshold = 50, Limit = 100 },
                new Budget { Id = 3, UserId = 2, WarningThreshold = 100, Limit = 100 }
            };
            var mockDbSet = GetMockDbSet(budgets);
            var mockContext = new Mock<ApplicationDatabaseContext>();
            mockContext.Setup(c => c.Set<Budget>()).Returns(mockDbSet.Object);

            var repository = new BudgetRepository(mockContext.Object);

            var result = await repository.GetBudgetsExceedingThresholdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        private static Mock<DbSet<T>> GetMockDbSet<T>(List<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var mockDbSet = new Mock<DbSet<T>>();
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            return mockDbSet;
        }
    }
}
