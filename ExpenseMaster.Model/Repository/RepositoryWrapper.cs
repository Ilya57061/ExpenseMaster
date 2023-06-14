using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.DatabaseContext;

namespace ExpenseMaster.BusinessLogic.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDatabaseContext _appContext;
        private IUserRepository _user;
        private IIncomeRepository _income;
        private IExpenceRepository _expence;

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

        public IIncomeRepository Income
        {
            get
            {
                if(_income == null)
                {
                    _income = new IncomeRepository(_appContext);
                }

                return _income;
            }
        }
        public IExpenceRepository Expence
        {
            get
            {
                if(_expence == null)
                {
                    _expence = new ExpenceRepository(_appContext);
                }

                return _expence;
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
