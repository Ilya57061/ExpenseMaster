using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Model.DatabaseContext;
using ExpenseMaster.Model.Models;

namespace ExpenseMaster.BusinessLogic.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDatabaseContext appContext)
            : base(appContext)
        {

        }
    }
}
