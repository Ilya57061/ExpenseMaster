using ExpenseMaster.BusinessLogic.Dto;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        Task<SuccesLoginDto> AuthenticateAsync(UserLoginDto userLoginDto);
    }
}
