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
    }
}
