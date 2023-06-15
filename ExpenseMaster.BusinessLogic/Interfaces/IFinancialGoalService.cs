using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IFinancialGoalService
    {
        Task<FinancialGoal> GetByIdAsync(int id);
        Task CreateAsync(FinancialGoal financialGoal);
        Task UpdateAsync(FinancialGoal financialGoal);
        Task DeleteAsync(FinancialGoal financialGoal);
        Task<IEnumerable<FinancialGoal>> GetByUserIdAsync(int userId);
        Task UpdateCurrentAmountAsync(int goalId, decimal currentAmount);
        Task<IEnumerable<FinancialGoal>> GetByTargetAmountAsync(int userId);
        Task<decimal> GetTotalProgressAsync(int userId);
    }
}
