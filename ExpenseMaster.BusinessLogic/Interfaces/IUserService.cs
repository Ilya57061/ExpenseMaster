using ExpenseMaster.BusinessLogic.Dto;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task DeleteUserAsync(int userId);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetUsersByLoginAsync(string login);
        Task<UserDto> UpdateUserAsync(UserUpdateDto user);
    }
}
