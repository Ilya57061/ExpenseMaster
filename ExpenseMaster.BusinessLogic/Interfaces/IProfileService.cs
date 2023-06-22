using ExpenseMaster.BusinessLogic.Dto;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IProfileService
    {
        Task DeleteProfileAsync(int userId);
        Task<ProfileDto> GetUserByIdAsync(int id);
        Task<ProfileDto> UpdatePasswordAsync(UpdateProfilePasswordDto updatePasswordDto, int userId);
        Task<ProfileDto> UpdateProfileAsync(ProfileDto profileDto, int userId);
    }
}
