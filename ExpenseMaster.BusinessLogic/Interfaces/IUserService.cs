using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task DeleteUserAsync(User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetUsersByLoginAsync(string login);
        Task<User> UpdateUserAsync(UserUpdateDto user);
    }
}
