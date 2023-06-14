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
    }
}
