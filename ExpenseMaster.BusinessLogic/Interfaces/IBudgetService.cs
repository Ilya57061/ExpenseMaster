using ExpenseMaster.BusinessLogic.Dto;

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
    }
}
