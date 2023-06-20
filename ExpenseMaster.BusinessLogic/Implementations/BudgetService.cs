using AutoMapper;
using ExpenseMaster.BusinessLogic.AbstractDto;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class BudgetService : IBudgetService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public BudgetService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<BudgetDto> GetByIdAsync(int id)
        {
            var budget = await _repositoryWrapper.Budget.GetByIdAsync(id);
            if (budget == null)
            {
                throw new InvalidOperationException($"Budget with id - {id} was not found");
            }
            var budgetDto = _mapper.Map<BudgetDto>(budget);

            return budgetDto;
        }

        public async Task CreateAsync(CreateBudgetDto budgetDto)
        {
            var budget = _mapper.Map<Budget>(budgetDto);
            await _repositoryWrapper.Budget.CreateAsync(budget);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task UpdateAsync(UpdateBudgetDto budgetDto)
        {
            var budget = _mapper.Map<Budget>(budgetDto);
            await _repositoryWrapper.Budget.UpdateAsync(budget);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var budget = await _repositoryWrapper.Budget.GetByIdAsync(id);
            if (budget == null)
            {
                throw new InvalidOperationException($"Budget with id - {id} was not found");
            }
            await _repositoryWrapper.Budget.DeleteAsync(budget);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task<IEnumerable<BudgetDto>> GetByUserIdAsync(int userId)
        {
            var budgets = await _repositoryWrapper.Budget.GetBudgetsByUserIdAsync(userId);
            if (budgets == null)
            {
                throw new InvalidOperationException($"Budgets with UserId - {userId} was not found");
            }
            var budgetDto = _mapper.Map<IEnumerable<BudgetDto>>(budgets);

            return budgetDto;
        }

        public async Task<IEnumerable<BudgetDto>> GetByCategoryIdAsync(int userId, int categoryId)
        {
            var budgets = await _repositoryWrapper.Budget.GetBudgetByCategoryIdAsync(userId, categoryId);
            var budgetDto = _mapper.Map<IEnumerable<BudgetDto>>(budgets);

            return budgetDto;
        }

        public async Task<IEnumerable<BudgetDto>> GetBudgetsExceedingThresholdAsync(int userId)
        {
            var budgets = await _repositoryWrapper.Budget.GetBudgetsExceedingThresholdAsync(userId);
            var budgetDto = _mapper.Map<IEnumerable<BudgetDto>>(budgets);

            return budgetDto;
        }

        public async Task UpdateWarningThresholdAsync(int budgetId, decimal warningThreshold)
        {
            var budget = await _repositoryWrapper.Budget.GetByIdAsync(budgetId);
            budget.WarningThreshold = warningThreshold;
            await _repositoryWrapper.Budget.UpdateAsync(budget);
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
