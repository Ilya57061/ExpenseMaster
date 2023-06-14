using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.DatabaseContext;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Repository
{
    public class ExpenceRepository : RepositoryBase<Expense>, IExpenceRepository
    {
        public ExpenceRepository(ApplicationDatabaseContext appContext)
            : base(appContext)
        {
        }
    }
}
