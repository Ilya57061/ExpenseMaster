using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class BudgetService : IBudgetService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public BudgetService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Budget> GetByIdAsync(int id)
        {
            var budget = await _repositoryWrapper.Budget.GetByIdAsync(id);

            return budget;
        }

        public async Task CreateAsync(CreateBudgetDto budgetDto)
        {
            var budget = _mapper.Map<Budget>(budgetDto);
            _repositoryWrapper.Budget.CreateAsync(budget);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task UpdateAsync(UpdateBudgetDto budgetDto)
        {
            var budget = _mapper.Map<Budget>(budgetDto);
            _repositoryWrapper.Budget.UpdateAsync(budget);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task DeleteAsync(Budget budget)
        {
            _repositoryWrapper.Budget.DeleteAsync(budget);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task<IEnumerable<Budget>> GetByUserIdAsync(int userId)
        {
            var budgets = await _repositoryWrapper.Budget.GetBudgetsByUserIdAsync(userId);

            return budgets;
        }

        public async Task<Budget> GetByCategoryIdAsync(int userId, int categoryId)
        {
            var budgets = await _repositoryWrapper.Budget.GetBudgetByCategoryIdAsync(userId, categoryId);

            return budgets;
        }

        public async Task<IEnumerable<Budget>> GetBudgetsExceedingThresholdAsync(int userId)
        {
            var budgets = await _repositoryWrapper.Budget.GetBudgetsExceedingThresholdAsync(userId);

            return budgets;
        }

        public async Task UpdateWarningThresholdAsync(int budgetId, decimal warningThreshold)
        {
            var budget = await _repositoryWrapper.Budget.GetByIdAsync(budgetId);
            budget.WarningThreshold = warningThreshold;
            _repositoryWrapper.Budget.UpdateAsync(budget);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task<decimal> GetBudgetRemainingAmountAsync(int userId)
        {
            var userBudgets = await _repositoryWrapper.Budget.GetBudgetsByUserIdAsync(userId);
            decimal totalExpenses = 0;

            foreach (var budget in userBudgets)
            {
                decimal categoryExpenses = await _repositoryWrapper.Expense.GetTotalExpensesByCategoryAsync(userId, budget.CategoryId);
                totalExpenses += categoryExpenses;
            }

            decimal totalBudget = userBudgets.Sum(b => b.Limit);
            decimal remainingAmount = totalBudget - totalExpenses;

            return remainingAmount;
        }
    }
}
