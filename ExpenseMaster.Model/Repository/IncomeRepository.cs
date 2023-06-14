using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.DatabaseContext;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Repository
{
    public class IncomeRepository : RepositoryBase<Income>, IIncomeRepository
    {
        public IncomeRepository(ApplicationDatabaseContext appContext)
            : base(appContext)
        {
        }
    }
}
