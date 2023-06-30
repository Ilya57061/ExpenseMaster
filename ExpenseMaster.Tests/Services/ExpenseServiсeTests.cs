using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Implementations;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;
using Moq;
using System.Linq.Expressions;
using Xunit;
using FluentAssertions;

namespace ExpenseMaster.Tests.Services
{
    public class ExpenseServiсeTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepositoryWrapper;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<INotificationService> _mockNotificationService;
        private readonly ExpenseService _expenseService;

        public ExpenseServiсeTests()
        {
            _mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            _mockMapper = new Mock<IMapper>();
            _mockNotificationService = new Mock<INotificationService>();
            _expenseService = new ExpenseService(_mockRepositoryWrapper.Object, _mockMapper.Object, _mockNotificationService.Object);
        }

        [Fact]
        public async Task GetAllExpenses_ReturnsListOfExpenseItemDto()
        {
            // Arrange
            var expenses = new List<Expense>
            {
                new Expense { Id = 1, Amount = 100 },
                new Expense { Id = 2, Amount = 200 }
            };
            var expenseItemDtos = new List<ExpenseItemDto>
            {
                new ExpenseItemDto { Id = 1, Amount = 100 },
                new ExpenseItemDto { Id = 2, Amount = 200 }
            };
            _mockRepositoryWrapper.Setup(r => r.Expence.FindAllAsync()).ReturnsAsync(expenses.AsQueryable());
            _mockMapper.Setup(m => m.Map<IEnumerable<ExpenseItemDto>>(expenses)).Returns(expenseItemDtos);

            // Act
            var result = await _expenseService.GetAllExpenses();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expenseItemDtos);
        }

        [Fact]
        public async Task GetExpenseById_NonExistingId_ThrowsInvalidOperationException()
        {
            // Arrange
            var id = 1;
            var result = Enumerable.Empty<Expense>().AsQueryable();
            _mockRepositoryWrapper.Setup(r => r.Expence.FindByConditionAsync(It.IsAny<Expression<Func<Expense, bool>>>()))
                .ReturnsAsync(result);

            // Act
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _expenseService.GetExpenseById(id));

            // Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task CreateExpense_NullExpenseDto_ThrowsArgumentNullException()
        {
            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _expenseService.CreateExpense(null));
        }

        [Fact]
        public async Task UpdateExpense_ExistingExpenseItemDto_UpdatesExpenseAndSavesChanges()
        {
            // Arrange
            var expenseItemDto = new ExpenseItemDto { Id = 1, UserId = 1, CategoryId = 1, Amount = 200 };
            var existingExpense = new List<Expense> { new Expense { Id = 1, UserId = 1, CategoryId = 1, Amount = 100 } }.AsQueryable();
            var updatedExpense = new Expense { Id = 1, UserId = 1, CategoryId = 1, Amount = 200 };

            _mockRepositoryWrapper.Setup(r => r.Expence.FindByConditionAsync(It.IsAny<Expression<Func<Expense, bool>>>()))
                .ReturnsAsync(existingExpense);
            _mockMapper.Setup(m => m.Map<Expense>(expenseItemDto)).Returns(updatedExpense);
            _mockRepositoryWrapper.Setup(r => r.Expence.UpdateAsync(It.IsAny<Expense>())).Returns(Task.CompletedTask);
            _mockRepositoryWrapper.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<ExpenseItemDto>(It.IsAny<Expense>())).Returns(expenseItemDto);

            // Act
            var result = await _expenseService.UpdateExpense(expenseItemDto);

            // Assert
            result.Should().NotBeNull()
                .And.BeOfType<ExpenseItemDto>()
                .And.BeEquivalentTo(expenseItemDto);

            _mockRepositoryWrapper.Verify(r => r.Expence.FindByConditionAsync(It.IsAny<Expression<Func<Expense, bool>>>()), Times.Once);
            _mockMapper.Verify(m => m.Map<Expense>(expenseItemDto), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.Expence.UpdateAsync(It.IsAny<Expense>()), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.SaveAsync(), Times.Once);
            _mockMapper.Verify(m => m.Map<ExpenseItemDto>(It.IsAny<Expense>()), Times.Once);
        }

        [Fact]
        public async Task UpdateExpense_NullExpenseItemDto_ThrowsArgumentNullException()
        {
            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _expenseService.UpdateExpense(null));
        }

        [Fact]
        public async Task DeleteExpense_ExistingExpenseItemDto_DeletesExpenseAndSavesChanges()
        {
            // Arrange
            var expenseItemDto = new ExpenseItemDto { Id = 1, UserId = 1, CategoryId = 1, Amount = 100 };
            var existingExpense = new List<Expense> { new Expense { Id = 1, UserId = 1, CategoryId = 1, Amount = 100 } }.AsQueryable();

            _mockRepositoryWrapper.Setup(r => r.Expence.FindByConditionAsync(It.IsAny<Expression<Func<Expense, bool>>>()))
                .ReturnsAsync(existingExpense);
            _mockRepositoryWrapper.Setup(r => r.Expence.DeleteAsync(It.IsAny<Expense>()));
            _mockRepositoryWrapper.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            await _expenseService.DeleteExpense(expenseItemDto);

            // Assert
            _mockRepositoryWrapper.Verify(r => r.Expence.FindByConditionAsync(It.IsAny<Expression<Func<Expense, bool>>>()), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.Expence.DeleteAsync(It.IsAny<Expense>()), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteExpense_NullExpenseItemDto_ThrowsArgumentNullException()
        {
            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _expenseService.DeleteExpense(null));
        }

        [Fact]
        public async Task GetExpensesByCategory_ValidCategoryId_ReturnsListOfExpenseDto()
        {
            // Arrange
            var categoryId = 1;
            var expenses = new List<Expense> { new Expense { Id = 1, Amount = 100 } };
            var expensesDto = new List<ExpenseDto> { new ExpenseDto { Amount = 100 } };
            _mockRepositoryWrapper.Setup(r => r.Expence.FindByConditionAsync(It.IsAny<Expression<Func<Expense, bool>>>()))
                .ReturnsAsync(expenses.AsQueryable());
            _mockMapper.Setup(m => m.Map<IEnumerable<ExpenseDto>>(expenses)).Returns(expensesDto);

            // Act
            var result = await _expenseService.GetExpensesByCategory(categoryId);

            // Assert
            result.Should().NotBeNull()
                .And.BeEquivalentTo(expensesDto);
        }

        [Fact]
        public async Task GetExpensesByCategory_InvalidCategoryId_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var categoryId = -1;

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _expenseService.GetExpensesByCategory(categoryId));
        }
    }
}