using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Model.DatabaseContext;
using ExpenseMaster.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMaster.BusinessLogic.Repository
{
    public class BudgetRepository : RepositoryBase<Budget>, IBudgetRepository
    {
        public BudgetRepository(ApplicationDatabaseContext appContext)
            : base(appContext)
        {
        }
        public async Task<Budget> GetByIdAsync(int id)
        {
            var result = await FindByConditionAsync(x => x.Id == id);
            var budget = result.FirstOrDefault();

            return budget;
        }
        public async Task<IEnumerable<Budget>> GetBudgetsByUserIdAsync(int userId)
        {
            var budgets = await FindByConditionAsync(b => b.UserId == userId);

            return budgets;
        }

        public async Task<Budget> GetBudgetByCategoryIdAsync(int userId, int categoryId)
        {

            var result = await FindByConditionAsync(b => b.UserId == userId && b.CategoryId == categoryId);
            var budget = result.FirstOrDefault();

            return budget;
        }

        public async Task<IEnumerable<Budget>> GetBudgetsExceedingThresholdAsync(int userId)
        {
            var budgets = await FindByConditionAsync(b => b.UserId == userId && b.WarningThreshold < b.Limit);

            return budgets;
        }
    }
}
