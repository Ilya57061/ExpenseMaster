using ExpenseMaster.BusinessLogic.AbstractDto;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IBudgetService
    {
        Task<IEnumerable<ReturnBudgetDto>> GetByCategoryIdAsync(int userId, int categoryId);
        Task<ReturnBudgetDto> GetByIdAsync(int id);
        Task CreateAsync(CreateBudgetDto budgetDto);
        Task UpdateAsync(UpdateBudgetDto budgetDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ReturnBudgetDto>> GetByUserIdAsync(int userId);
        Task UpdateWarningThresholdAsync(int budgetId, decimal warningThreshold);
        Task<IEnumerable<ReturnBudgetDto>> GetBudgetsExceedingThresholdAsync(int userId);
        Task<decimal> GetBudgetRemainingAmountAsync(int userId);
    }
}
