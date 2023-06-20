using ExpenseMaster.BusinessLogic.AbstractDto;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IBudgetService
    {
        Task<IEnumerable<BudgetDto>> GetByCategoryIdAsync(int userId, int categoryId);
        Task<BudgetDto> GetByIdAsync(int id);
        Task CreateAsync(CreateBudgetDto budgetDto);
        Task UpdateAsync(UpdateBudgetDto budgetDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<BudgetDto>> GetByUserIdAsync(int userId);
        Task UpdateWarningThresholdAsync(int budgetId, decimal warningThreshold);
        Task<IEnumerable<BudgetDto>> GetBudgetsExceedingThresholdAsync(int userId);
        Task<decimal> GetBudgetRemainingAmountAsync(int userId);
    }
}
