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
        private readonly Mock<ApplicationDatabaseContext> mockContext;
        private readonly BudgetRepository repository;
        private readonly List<Budget> budgets;

        public BudgetRepositoryTests()
        {
            mockContext = new Mock<ApplicationDatabaseContext>();
            repository = new BudgetRepository(mockContext.Object);
            budgets = InitializeBudgets();
        }



        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsBudget()
        {
            // Arrange
            var mockDbSet = GetMockDbSet(budgets);
            mockContext.Setup(c => c.Set<Budget>()).Returns(mockDbSet.Object);

            // Act
            var result = await repository.GetByIdAsync(2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
        }

        [Fact]
        public async Task GetBudgetsByUserIdAsync_ExistingUserId_ReturnsBudgets()
        {
            // Arrange
            var mockDbSet = GetMockDbSet(budgets);
            mockContext.Setup(c => c.Set<Budget>()).Returns(mockDbSet.Object);

            // Act
            var result = await repository.GetBudgetsByUserIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetBudgetByCategoryIdAsync_ExistingUserIdAndCategoryId_ReturnsBudget()
        {
            // Arrange
            var mockDbSet = GetMockDbSet(budgets);
            mockContext.Setup(c => c.Set<Budget>()).Returns(mockDbSet.Object);

            // Act
            var result = await repository.GetBudgetByCategoryIdAsync(1, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
        }

        [Fact]
        public async Task GetBudgetsExceedingThresholdAsync_ExistingUserId_ReturnsBudgets()
        {
            // Arrange
            var mockDbSet = GetMockDbSet(budgets);
            mockContext.Setup(c => c.Set<Budget>()).Returns(mockDbSet.Object);

            var threshold = 100;
            var expectedBudgets = budgets
                .Where(b => b.UserId == 1 && b.Limit > threshold)
                .ToList();

            // Act
            var result = await repository.GetBudgetsExceedingThresholdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedBudgets.Count, result.Count());
            Assert.Equal(expectedBudgets, result);
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

        private List<Budget> InitializeBudgets()
        {
            return new List<Budget> 
            { 
                new Budget { Id = 1, UserId = 1, CategoryId = 1 }, 
                new Budget { Id = 2, UserId = 1, CategoryId = 2 }, 
                new Budget { Id = 3, UserId = 2, CategoryId = 1 } 
            };
        }
    }
}
