using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Model.DatabaseContext;

namespace ExpenseMaster.BusinessLogic.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDatabaseContext _appContext;
        private IUserRepository _user;

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
