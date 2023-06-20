using ExpenseMaster.DAL.DatabaseContext;
using ExpenseMaster.DAL.Interfaces;
using ExpenseMaster.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMaster.DAL.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDatabaseContext appContext)
            : base(appContext)
        {

        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            var result = await FindByConditionAsync(u => u.Login == login);
            var user = await result.Include(u => u.Role)
        .AsNoTracking()
        .SingleOrDefaultAsync();

            return user;
        }
    }
}
