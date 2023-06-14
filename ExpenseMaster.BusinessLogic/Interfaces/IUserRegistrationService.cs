using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IUserRegistrationService
    {
        Task<User> RegisterAsync(UserRegistrationDto userRegistrationDto);
    }
}
