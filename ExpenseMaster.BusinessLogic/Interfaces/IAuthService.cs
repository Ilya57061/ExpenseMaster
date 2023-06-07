using ExpenseMaster.Common.Dto;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> AuthenticateAsync(UserLoginDto userLoginDto);
    }
}
