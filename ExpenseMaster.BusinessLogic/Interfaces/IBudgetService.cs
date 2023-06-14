using ExpenseMaster.Model.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IBudgetService
    {
        Task GetById(int id);
        Task Create(Budget budget);
        Task Update(Budget budget);
        Task Delete(Budget budget);
        Task<IEnumerable<Budget>> GetByUserId(int userId);
        Task<List<Budget>> GetBudgetsExceedingThreshold(int userId);
        Task UpdateWarningThreshold(int budgetId, decimal warningThreshold);
        Task<decimal> GetBudgetRemainingAmount(int userId);
    }
}
