using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.DAL.Interfaces
{
    public interface IBudgetRepository : IRepositoryBase<Budget>
    {
        Task<Budget> GetByIdAsync(int id);
        Task<IEnumerable<Budget>> GetBudgetsByUserIdAsync(int userId);
        Task<Budget> GetBudgetByCategoryIdAsync(int userId, int categoryId);
        Task<IEnumerable<Budget>> GetBudgetsExceedingThresholdAsync(int userId);
    }
}
