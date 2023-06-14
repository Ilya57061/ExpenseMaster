﻿using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Model.Models;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class BudgetService : IBudgetService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public BudgetService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<Budget> GetByIdAsync(int id)
        {
            return await _repositoryWrapper.Budget.GetByIdAsync(id);
        }
        public async Task CreateAsync(Budget budget)
        {
            _repositoryWrapper.Budget.CreateAsync(budget);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task UpdateAsync(Budget budget)
        {
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
            return await _repositoryWrapper.Budget.GetBudgetsByUserIdAsync(userId);
        }

        public async Task<Budget> GetByCategoryIdAsync(int userId, int categoryId)
        {
            return await _repositoryWrapper.Budget.GetBudgetByCategoryIdAsync(userId, categoryId);
        }

        public async Task<IEnumerable<Budget>> GetBudgetsExceedingThresholdAsync(int userId)
        {
            return await _repositoryWrapper.Budget.GetBudgetsExceedingThresholdAsync(userId);
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
