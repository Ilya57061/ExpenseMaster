using ExpenseMaster.BusinessLogic.Dto;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IProfileService
    {
        Task DeleteProfileAsync(UserRegistrationDto existingUserDto);
        Task<UserRegistrationDto> GetUserByIdAsync(int id);
        Task<UserRegistrationDto> UpdateProfileAsync(UserRegistrationDto userRegistrationDto, int userId);
    }
}
