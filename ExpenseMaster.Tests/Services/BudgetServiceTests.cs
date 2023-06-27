using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Implementations;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Interfaces;
using ExpenseMaster.DAL.Models;
using Moq;
using Xunit;

namespace ExpenseMaster.Tests.Services
{
    public class BudgetServiceTests
    {
        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsBudgetDto()
        {
            // Arrange
            var budgetId = 1;
            var mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            var mockMapper = new Mock<IMapper>();

            var budget = new Budget { Id = budgetId };
            var returnBudgetDto = new ReturnBudgetDto();

            mockRepositoryWrapper
                .Setup(r => r.Budget.GetByIdAsync(budgetId))
                .ReturnsAsync(budget);
            mockMapper
                .Setup(m => m.Map<ReturnBudgetDto>(budget))
                .Returns(returnBudgetDto);

            var budgetService = new BudgetService(mockRepositoryWrapper.Object, mockMapper.Object);

            // Act
            var result = await budgetService.GetByIdAsync(budgetId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(returnBudgetDto, result);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingId_ThrowsInvalidOperationException()
        {
            // Arrange
            var budgetId = 1;
            var mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            var mockMapper = new Mock<IMapper>();

            mockRepositoryWrapper
                .Setup(r => r.Budget.GetByIdAsync(budgetId))
                .ReturnsAsync((Budget)null);

            var budgetService = new BudgetService(mockRepositoryWrapper.Object, mockMapper.Object);

            // Act and Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => budgetService.GetByIdAsync(budgetId));
        }

        [Fact]
        public async Task CreateAsync_ValidBudgetDto_CallsRepositoryCreateAsyncAndSaveAsync()
        {
            // Arrange
            var createBudgetDto = new CreateBudgetDto();
            var mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            var mockBudgetRepository = new Mock<IBudgetRepository>();
            var mockMapper = new Mock<IMapper>();

            var budget = new Budget();
            mockMapper
                .Setup(m => m.Map<Budget>(createBudgetDto))
                .Returns(budget);

            mockRepositoryWrapper.Setup(r => r.Budget).Returns(mockBudgetRepository.Object);

            var budgetService = new BudgetService(mockRepositoryWrapper.Object, mockMapper.Object);

            // Act
            await budgetService.CreateAsync(createBudgetDto);

            // Assert
            mockBudgetRepository.Verify(r => r.CreateAsync(budget), Times.Once);
            mockRepositoryWrapper.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ValidBudgetDto_CallsRepositoryUpdateAsyncAndSaveAsync()
        {
            // Arrange
            var updateBudgetDto = new UpdateBudgetDto();
            var mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            var mockBudgetRepository = new Mock<IBudgetRepository>();
            var mockMapper = new Mock<IMapper>();

            var budget = new Budget();
            mockMapper
                .Setup(m => m.Map<Budget>(updateBudgetDto))
                .Returns(budget);

            mockRepositoryWrapper.Setup(r => r.Budget).Returns(mockBudgetRepository.Object);

            var budgetService = new BudgetService(mockRepositoryWrapper.Object, mockMapper.Object);

            // Act
            await budgetService.UpdateAsync(updateBudgetDto);

            // Assert
            mockBudgetRepository.Verify(r => r.UpdateAsync(budget), Times.Once);
            mockRepositoryWrapper.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ExistingId_CallsRepositoryDeleteAsyncAndSaveAsync()
        {
            // Arrange
            var budgetId = 1;
            var mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            var mockBudgetRepository = new Mock<IBudgetRepository>();
            var mockMapper = new Mock<IMapper>();

            var budget = new Budget { Id = budgetId };
            mockRepositoryWrapper
                .Setup(r => r.Budget.GetByIdAsync(budgetId))
                .ReturnsAsync(budget);

            mockBudgetRepository.Setup(r => r.GetByIdAsync(budgetId)).ReturnsAsync(budget);

            mockRepositoryWrapper.Setup(r => r.Budget).Returns(mockBudgetRepository.Object);

            var budgetService = new BudgetService(mockRepositoryWrapper.Object, mockMapper.Object);

            // Act
            await budgetService.DeleteAsync(budgetId);

            // Assert
            mockBudgetRepository.Verify(r => r.DeleteAsync(budget), Times.Once);
            mockRepositoryWrapper.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_NonExistingId_ThrowsInvalidOperationException()
        {
            // Arrange
            var budgetId = 1;
            var mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            var mockMapper = new Mock<IMapper>();

            mockRepositoryWrapper
                .Setup(r => r.Budget.GetByIdAsync(budgetId))
                .ReturnsAsync((Budget)null);

            var budgetService = new BudgetService(mockRepositoryWrapper.Object, mockMapper.Object);

            // Act and Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => budgetService.DeleteAsync(budgetId));
        }

        [Fact]
        public async Task GetByUserIdAsync_ExistingUserId_ReturnsBudgetDtos()
        {
            // Arrange
            var userId = 1;
            var mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            var mockMapper = new Mock<IMapper>();

            var budgets = new List<Budget> { new Budget(), new Budget() };
            var returnBudgetDtos = new List<ReturnBudgetDto> { new ReturnBudgetDto(), new ReturnBudgetDto() };

            mockRepositoryWrapper
                .Setup(r => r.Budget.GetBudgetsByUserIdAsync(userId))
                .ReturnsAsync(budgets);
            mockMapper
                .Setup(m => m.Map<IEnumerable<ReturnBudgetDto>>(budgets))
                .Returns(returnBudgetDtos);

            var budgetService = new BudgetService(mockRepositoryWrapper.Object, mockMapper.Object);

            // Act
            var result = await budgetService.GetByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(returnBudgetDtos.Count, result.Count());
        }

        [Fact]
        public async Task GetByUserIdAsync_NonExistingUserId_ThrowsInvalidOperationException()
        {
            // Arrange
            var userId = 1;
            var mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            var mockMapper = new Mock<IMapper>();

            mockRepositoryWrapper
                .Setup(r => r.Budget.GetBudgetsByUserIdAsync(userId))
                .ReturnsAsync((List<Budget>)null);

            var budgetService = new BudgetService(mockRepositoryWrapper.Object, mockMapper.Object);

            // Act and Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => budgetService.GetByUserIdAsync(userId));
        }

        [Fact]
        public async Task GetBudgetsExceedingThresholdAsync_ExistingUserId_ReturnsBudgetDtos()
        {
            // Arrange
            var userId = 1;
            var mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            var mockMapper = new Mock<IMapper>();

            var budgets = new List<Budget> { new Budget(), new Budget() };
            var returnBudgetDtos = new List<ReturnBudgetDto> { new ReturnBudgetDto(), new ReturnBudgetDto() };

            mockRepositoryWrapper
                .Setup(r => r.Budget.GetBudgetsExceedingThresholdAsync(userId))
                .ReturnsAsync(budgets);
            mockMapper
                .Setup(m => m.Map<IEnumerable<ReturnBudgetDto>>(budgets))
                .Returns(returnBudgetDtos);

            var budgetService = new BudgetService(mockRepositoryWrapper.Object, mockMapper.Object);

            // Act
            var result = await budgetService.GetBudgetsExceedingThresholdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(returnBudgetDtos.Count, result.Count());
        }
    }
}