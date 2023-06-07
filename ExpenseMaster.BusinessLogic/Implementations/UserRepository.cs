using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Model.Models;
using ExpenseMaster.Model.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDataBaseContext _dbContext;
        public UserRepository(ApplicationDataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateUserAsync(User user)
        {
            try
            {
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ошибка при создании user", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Произошла ошибка в процессе создания пользователя", ex);
            }
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            try
            {
                return await _dbContext.Users.Include(x => x.Role).AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ошибка при получении user", ex);
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
                throw new Exception("Ошибка при получении user по login", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Произошла ошибка при получении пользователя по электронной почте '{login}'", ex);
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
                throw new Exception("Ошибка при получении user по mail", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Произошла ошибка при получении пользователя по электронной почте '{email}'", ex);
            }
        }

        public async Task UpdateUserAsync(User user)
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
                throw new Exception("Ошибка в процессе обновления пользователя", ex);
            }
        }

        public async Task DeleteUserAsync(User user)
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
                throw new Exception("Произошла в процессе удаления пользователя", ex);
            }
        }
    }
}
