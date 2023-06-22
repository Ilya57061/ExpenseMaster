using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.DatabaseContext;
using ExpenseMaster.DAL.Interfaces;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.DAL.Repository
{
    public class ExpenceRepository : RepositoryBase<Expense>, IExpenceRepository
    {
        public ExpenceRepository(ApplicationDatabaseContext appContext)
            : base(appContext)
        {
        }
        public async Task<decimal> CalculateTotalExpensesByUserId(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be a positive integer.");
            }

            var expenses = await FindByConditionAsync(x => x.UserId == userId);
            decimal totalExpenses = expenses.Sum(x => x.Amount);

            return totalExpenses;
        }
    }
}
