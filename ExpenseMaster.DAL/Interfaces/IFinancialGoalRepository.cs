using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.DAL.Interfaces
{
    public interface IFinancialGoalRepository : IRepositoryBase<FinancialGoal>
    {
        Task<FinancialGoal> GetByIdAsync(int id);
        Task<IEnumerable<FinancialGoal>> GetByUserIdAsync(int userId);
    }
}
