using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Model.DatabaseContext;
using ExpenseMaster.Model.Models;

namespace ExpenseMaster.BusinessLogic.Repository
{
    public class BudgetRepository : RepositoryBase<Budget>, IBudgetRepository
    {
        public BudgetRepository(ApplicationDatabaseContext appContext) 
            : base(appContext)
        {
        }
    }
}
