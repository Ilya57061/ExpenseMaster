using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.DatabaseContext;
using ExpenseMaster.DAL.Interfaces;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.DAL.Repository
{
    public class IncomeRepository : RepositoryBase<Income>, IIncomeRepository
    {
        public IncomeRepository(ApplicationDatabaseContext appContext)
            : base(appContext)
        {
        }
        public async Task<decimal> CalculateTotalIncomeByUserId(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be a positive integer.");
            }

            var incomes = await FindByConditionAsync(x => x.UserId == userId);
            decimal totalIncome = incomes.Sum(x => x.Amount);

            return totalIncome;
        }
    }
}
