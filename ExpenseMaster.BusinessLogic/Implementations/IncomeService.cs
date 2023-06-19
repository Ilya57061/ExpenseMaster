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

        public async Task<IEnumerable<Income>> GetAllIncomes()
        {
            return await _repositoryWrapper.Income.FindAllAsync();
        }

        public async Task<Income> GetIncomeById(int id)
        {
            var result = await _repositoryWrapper.Income.FindByConditionAsync(x => x.Id == id);
            var income = await result.FirstOrDefaultAsync();

            return income;
        }

        public async Task<Income> CreateIncome(IncomeDto incomeDto)
        {
            var income = _mapper.Map<Income>(incomeDto);

            await _repositoryWrapper.Income.CreateAsync(income);
            await _repositoryWrapper.SaveAsync();

            return income;
        }

        public async Task<Income> UpdateIncome(IncomeWithIdDto incomeWithIdDto)
        {
            var existingIncome = await _repositoryWrapper.Income.FindByConditionAsync(x => x.Id == incomeWithIdDto.Id);
            if (existingIncome == null)
            {
                throw new InvalidOperationException($"Income with id - {incomeWithIdDto.Id} not found");
            }

            var income = _mapper.Map<Income>(incomeWithIdDto);

            await _repositoryWrapper.Income.UpdateAsync(income);
            await _repositoryWrapper.SaveAsync();

            return income;
        }

        public async Task DeleteIncome(Income income)
        {
            var existingIncome = await _repositoryWrapper.Income.FindByConditionAsync(x => x.Id == income.Id);
            var incomeToDelete = await existingIncome.FirstOrDefaultAsync();
            if (incomeToDelete != null)
            {
                await _repositoryWrapper.Income.DeleteAsync(incomeToDelete);
                await _repositoryWrapper.SaveAsync();
            }
        }

        public async Task<IEnumerable<Income>> GetIncomesByCategory(int categoryId)
        {
            var incomes = await _repositoryWrapper.Income.FindByConditionAsync(x => x.CategoryId == categoryId);

            return incomes.ToList();
        }

        public async Task<decimal> CalculateTotalIncomeByUserId(int userId)
        {
            var incomes = await _repositoryWrapper.Income.FindByConditionAsync(x => x.UserId == userId);
            decimal totalIncome = incomes.Sum(x => x.Amount);

            return totalIncome;
        }
    }
}
