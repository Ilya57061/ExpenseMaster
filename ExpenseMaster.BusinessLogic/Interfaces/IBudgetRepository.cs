using ExpenseMaster.Model.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IBudgetRepository : IRepositoryBase<Budget>
    {
        Task<Budget> GetByIdAsync(int id);
        Task<IEnumerable<Budget>> GetBudgetsByUserIdAsync(int userId);
        Task<Budget> GetBudgetByCategoryIdAsync(int userId, int categoryId);
        Task<IEnumerable<Budget>> GetBudgetsExceedingThresholdAsync(int userId);


    }
}
