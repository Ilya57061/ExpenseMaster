using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class IncomeService : IIncomeService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public IncomeService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
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

        public async Task CreateIncome(Income income)
        {
            await _repositoryWrapper.Income.CreateAsync(income);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task UpdateIncome(Income income)
        {
            await _repositoryWrapper.Income.UpdateAsync(income);
            await _repositoryWrapper.SaveAsync();
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
