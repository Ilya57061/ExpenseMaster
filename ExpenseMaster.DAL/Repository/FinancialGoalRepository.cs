using ExpenseMaster.DAL.DatabaseContext;
using ExpenseMaster.DAL.Interfaces;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.DAL.Repository
{
    public class FinancialGoalRepository : RepositoryBase<FinancialGoal>, IFinancialGoalRepository
    {
        public FinancialGoalRepository(ApplicationDatabaseContext appContext) 
            : base(appContext)
        {
            
        }
        public async Task<FinancialGoal> GetByIdAsync(int id)
        {
            var result = await FindByConditionAsync(x => x.Id == id);
            var financialGoal = result.FirstOrDefault();

            return financialGoal;
        }

        public async Task<IEnumerable<FinancialGoal>> GetByUserIdAsync(int userId)
        {
            var budgets = await FindByConditionAsync(b => b.UserId == userId);

            return budgets;
        }
    }
}
