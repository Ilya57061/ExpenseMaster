using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IProfileService
    {
        Task DeleteProfileAsync(User user);
        Task<User> UpdateProfileAsync(UserRegistrationDto userRegistrationDto, User existingUser);
    }
}
