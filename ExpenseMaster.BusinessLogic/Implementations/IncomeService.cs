using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Model.Models;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;

        public IncomeService(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public async Task<IEnumerable<Income>> GetIncomesByCategory(int categoryId)
        {
            var incomes = await _incomeRepository.FindByConditionAsync(x => x.CategoryId == categoryId);

            return incomes.ToList();
        }

        public async Task<decimal> CalculateTotalIncomeByUserId(int userId)
        {
            var incomes = await _incomeRepository.FindByConditionAsync(x => x.UserId == userId);
            decimal totalIncome = incomes.Sum(x => x.Amount);

            return totalIncome;
        }
    }
}
