using ExpenseMaster.Common.Dto;
using ExpenseMaster.Model.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IUserRegistrationService
    {
        Task<User> RegisterAsync(UserRegistrationDto userRegistrationDto);
    }
}
