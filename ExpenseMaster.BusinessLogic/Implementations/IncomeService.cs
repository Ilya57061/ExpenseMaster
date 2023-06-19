using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class IncomeService : IIncomeService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public IncomeService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IncomeWithIdDto>> GetAllIncomes()
        {
            var incomes =  await _repositoryWrapper.Income.FindAllAsync();
            var incomesWithIdDto = _mapper.Map<IEnumerable<IncomeWithIdDto>>(incomes);

            return incomesWithIdDto;
        }

        public async Task<IncomeWithIdDto> GetIncomeById(int id)
        {
            var result = await _repositoryWrapper.Income.FindByConditionAsync(x => x.Id == id);
            var income = await result.FirstOrDefaultAsync();
            var incomeDto = _mapper.Map<IncomeWithIdDto>(income);

            return incomeDto;
        }

        public async Task<IncomeDto> CreateIncome(IncomeDto incomeDto)
        {
            if (incomeDto == null)
            {
                throw new ArgumentNullException(nameof(incomeDto));
            }

            var income = _mapper.Map<Income>(incomeDto);

            await _repositoryWrapper.Income.CreateAsync(income);
            await _repositoryWrapper.SaveAsync();

            return incomeDto;
        }

        public async Task<IncomeWithIdDto> UpdateIncome(IncomeWithIdDto incomeWithIdDto)
        {
            if (incomeWithIdDto == null)
            {
                throw new ArgumentNullException(nameof(incomeWithIdDto));
            }

            var existingIncome = await _repositoryWrapper.Income.FindByConditionAsync(x => x.Id == incomeWithIdDto.Id);

            if (existingIncome == null)
            {
                throw new InvalidOperationException($"Income with id - {incomeWithIdDto.Id} not found");
            }

            var income = _mapper.Map<Income>(incomeWithIdDto);

            await _repositoryWrapper.Income.UpdateAsync(income);
            await _repositoryWrapper.SaveAsync();

            var updateIncome = _mapper.Map<IncomeWithIdDto>(income);

            return updateIncome;
        }

        public async Task DeleteIncome(IncomeWithIdDto incomeWithIdDto)
        {
            if (incomeWithIdDto == null)
            {
                throw new ArgumentNullException(nameof(incomeWithIdDto));
            }

            var existingIncome = await _repositoryWrapper.Income.FindByConditionAsync(x => x.Id == incomeWithIdDto.Id);
            var incomeToDelete = await existingIncome.FirstOrDefaultAsync();

            if (incomeToDelete != null)
            {
                await _repositoryWrapper.Income.DeleteAsync(incomeToDelete);
                await _repositoryWrapper.SaveAsync();
            }
        }

        public async Task<IEnumerable<IncomeDto>> GetIncomesByCategory(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(categoryId), "Category ID must be a positive integer.");
            }

            var incomes = await _repositoryWrapper.Income.FindByConditionAsync(x => x.CategoryId == categoryId);
            var incomesDto = _mapper.Map<IEnumerable<IncomeDto>>(incomes);

            return incomesDto;
        }

        public async Task<decimal> CalculateTotalIncomeByUserId(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be a positive integer.");
            }

            var incomes = await _repositoryWrapper.Income.FindByConditionAsync(x => x.UserId == userId);
            decimal totalIncome = incomes.Sum(x => x.Amount);

            return totalIncome;
        }
    }
}
