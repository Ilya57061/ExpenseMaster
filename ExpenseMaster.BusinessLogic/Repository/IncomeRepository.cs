using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Model.DatabaseContext;
using ExpenseMaster.Model.Models;

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
