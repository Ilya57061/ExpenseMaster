using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IBudgetService
    {
        Task<Budget> GetByCategoryIdAsync(int userId, int categoryId);
        Task<Budget> GetByIdAsync(int id);
        Task CreateAsync(CreateBudgetDto budgetDto);
        Task UpdateAsync(UpdateBudgetDto budgetDto);
        Task DeleteAsync(Budget budget);
        Task<IEnumerable<Budget>> GetByUserIdAsync(int userId);
        Task UpdateWarningThresholdAsync(int budgetId, decimal warningThreshold);
        Task<IEnumerable<Budget>> GetBudgetsExceedingThresholdAsync(int userId);
        Task<decimal> GetBudgetRemainingAmountAsync(int userId);
    }
}
