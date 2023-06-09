using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Model.Models;
using ExpenseMaster.Model.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class UserRepository : IRepository<User>
    {
        private readonly ApplicationDatabaseContext _dbContext;
        public UserRepository(ApplicationDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(User user)
        {
            try
            {
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ошибка при создании пользователя", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Произошла ошибка в процессе создания пользователя", ex);
            }
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            try
            {
                return await _dbContext.Users.Include(x => x.Role).AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Ошибка при получении пользователя с идентификатором '{userId}'", ex) ;
            }
            catch (Exception ex)
            {
                throw new Exception($"Произошла ошибка при получении пользователя с идентификатором '{userId}'", ex);
            }
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            try
            {
                return await _dbContext.Users.Include(x => x.Role).AsNoTracking().FirstOrDefaultAsync(u => u.Login == login);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ошибка при получении пользователя по логину", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Произошла ошибка при получении пользователя по логину '{login}'", ex);
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _dbContext.Users.Include(x => x.Role).AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ошибка при получении пользователя по электронной почте", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Произошла ошибка при получении пользователя по электронной почте '{email}'", ex);
            }
        }

        public async Task UpdateAsync(User user)
        {
            try
            {
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Не удалось обновить пользователя", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Произошла ошибка в процессе обновления пользователя", ex);
            }
        }

        public async Task DeleteAsync(User user)
        {
            try
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Не удалось удалить пользователя", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Произошла ошибка в процессе удаления пользователя", ex);
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при получении всех пользователей", ex);
            }
        }
    }
}
