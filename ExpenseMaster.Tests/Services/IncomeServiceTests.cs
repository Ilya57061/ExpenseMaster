using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Implementations;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace ExpenseMaster.Tests.Services
{
    public class IncomeServiceTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepositoryWrapper;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IncomeService _incomeService;

        public IncomeServiceTests()
        {
            _mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            _mockMapper = new Mock<IMapper>();
            _incomeService = new IncomeService(_mockRepositoryWrapper.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllIncomes_ReturnsListOfIncomeItemDto()
        {
            // Arrange
            var incomes = new List<Income>
            {
                new Income { Id = 1, Amount = 100 },
                new Income { Id = 2, Amount = 200 }
            };
            var incomeItemDtos = new List<IncomeItemDto>
            {
                new IncomeItemDto { Id = 1, Amount = 100 },
                new IncomeItemDto { Id = 2, Amount = 200 }
            };
            _mockRepositoryWrapper.Setup(r => r.Income.FindAllAsync()).Returns(() => Task.FromResult(incomes.AsQueryable()));
            _mockMapper.Setup(m => m.Map<IEnumerable<IncomeItemDto>>(incomes)).Returns(incomeItemDtos);

            // Act
            var result = await _incomeService.GetAllIncomes();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(incomeItemDtos);
        }

        [Fact]
        public async Task GetIncomeById_ExistingId_ReturnsIncomeItemDto()
        {
            // Arrange
            var id = 1;
            var income = new Income { Id = id, Amount = 100 };
            var incomeItemDto = new IncomeItemDto { Id = id, Amount = 100 };
            var result = new List<Income> { income }.AsQueryable();
            _mockRepositoryWrapper.Setup(r => r.Income.FindByConditionAsync(It.IsAny<Expression<Func<Income, bool>>>()))
                .Returns(Task.FromResult(result));
            _mockMapper.Setup(m => m.Map<IncomeItemDto>(income)).Returns(incomeItemDto);

            // Act
            var returnedIncomeItemDto = await _incomeService.GetIncomeById(id);

            // Assert
            returnedIncomeItemDto.Should().NotBeNull();
            returnedIncomeItemDto.Should().BeOfType<IncomeItemDto>();
            returnedIncomeItemDto.Should().BeEquivalentTo(incomeItemDto);
        }

        [Fact]
        public async Task GetIncomeById_NonExistingId_ThrowsInvalidOperationException()
        {
            // Arrange
            var id = 1;
            var result = Enumerable.Empty<Income>().AsQueryable();
            _mockRepositoryWrapper.Setup(r => r.Income.FindByConditionAsync(It.IsAny<Expression<Func<Income, bool>>>()))
                .Returns(Task.FromResult(result));

            // Act and Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _incomeService.GetIncomeById(id));
        }

        [Fact]
        public async Task CreateIncome_ValidIncomeDto_ReturnsCreatedIncomeItemDto()
        {
            // Arrange
            var incomeDto = new IncomeDto { Amount = 100, CategoryId = 1, UserId = 1 };
            var createdIncomeItemDto = new IncomeItemDto { Id = 1, Amount = 100, UserId = 1, CategoryId = 1 };

            _mockMapper.Setup(m => m.Map<Income>(incomeDto)).Returns(new Income());
            _mockRepositoryWrapper.Setup(r => r.Income.CreateAsync(It.IsAny<Income>())).Returns(Task.CompletedTask);
            _mockRepositoryWrapper.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<IncomeItemDto>(It.IsAny<Income>())).Returns(createdIncomeItemDto);

            // Act
            var result = await _incomeService.CreateIncome(incomeDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<IncomeItemDto>();
            result.Should().BeEquivalentTo(createdIncomeItemDto);
        }

        [Fact]
        public async Task CreateIncome_NullIncomeDto_ThrowsArgumentNullException()
        {
            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _incomeService.CreateIncome(null));
        }

        [Fact]
        public async Task DeleteIncome_NullIncomeItemDto_ThrowsArgumentNullException()
        {
            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _incomeService.DeleteIncome(null));
        }

        [Fact]
        public async Task GetIncomesByCategory_ValidCategoryId_ReturnsListOfIncomeDto()
        {
            // Arrange
            var categoryId = 1;
            var incomes = new List<Income> { new Income { Id = 1, Amount = 100 } };
            var incomesDto = new List<IncomeDto> { new IncomeDto { Amount = 100 } };
            _mockRepositoryWrapper.Setup(r => r.Income.FindByConditionAsync(It.IsAny<Expression<Func<Income, bool>>>()))
                .Returns(Task.FromResult(incomes.AsQueryable()));
            _mockMapper.Setup(m => m.Map<IEnumerable<IncomeDto>>(incomes)).Returns(incomesDto);

            // Act
            var result = await _incomeService.GetIncomesByCategory(categoryId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(incomesDto);
        }

        [Fact]
        public async Task GetIncomesByCategory_InvalidCategoryId_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var categoryId = -1;

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _incomeService.GetIncomesByCategory(categoryId));
        }

        [Fact]
        public async Task CalculateTotalIncomeByUserId_ValidUserId_ReturnsTotalIncome()
        {
            // Arrange
            var userId = 1;
            var totalIncome = 500m;
            _mockRepositoryWrapper.Setup(r => r.Income.CalculateTotalIncomeByUserId(userId)).ReturnsAsync(totalIncome);

            // Act
            var result = await _incomeService.CalculateTotalIncomeByUserId(userId);

            // Assert
            result.Should().Be(totalIncome);
        }
    }
}