using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Model.Models;
using ExpenseMaster.Model.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDatabaseContext _dbContext;

        public UserRepository(ApplicationDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(User user)
        {
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(int userId)
        {
                return await _dbContext.Users.Include(x => x.Role).AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
                return await _dbContext.Users.Include(x => x.Role).AsNoTracking().FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
                return await _dbContext.Users.Include(x => x.Role).AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task UpdateAsync(User user)
        {
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
                return await _dbContext.Users.ToListAsync();
        }
    }
}
