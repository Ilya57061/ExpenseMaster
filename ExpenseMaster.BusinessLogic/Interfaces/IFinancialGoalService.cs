using ExpenseMaster.BusinessLogic.Dto;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IFinancialGoalService
    {
        Task<ReturnFinancialGoalDto> GetByIdAsync(int id);
        Task CreateAsync(CreateFinancialGoalDto financialGoal);
        Task UpdateAsync(UpdateFinancialGoalDto financialGoal);
        Task DeleteAsync(int id);
        Task<IEnumerable<ReturnFinancialGoalDto>> GetByUserIdAsync(int userId);
        Task UpdateCurrentAmountAsync(int goalId, decimal currentAmount);
        Task<IEnumerable<ReturnFinancialGoalDto>> GetByTargetAmountAsync(int userId);
        Task<decimal> GetTotalProgressAsync(int userId);
    }
}
