using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Model.DatabaseContext;

namespace ExpenseMaster.BusinessLogic.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDatabaseContext _appContext;
        private IUserRepository _user;
        private IBudgetRepository _budget;

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_appContext);
                }

                return _user;
            }
        }

        public IBudgetRepository Budget
        {
            get
            {
                if (_budget == null)
                {
                    _budget = new BudgetRepository(_appContext);
                }
                return _budget;
            }
        }

        public RepositoryWrapper(ApplicationDatabaseContext appContext)
        {
            _appContext = appContext;
        }

        public async Task SaveAsync()
        {
            await _appContext.SaveChangesAsync();
        }
    }
}
