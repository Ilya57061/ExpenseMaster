using ExpenseMaster.BusinessLogic.AbstractDto;

namespace ExpenseMaster.BusinessLogic.Dto
{
    public class UserRegistrationDto : BaseUserDto
    {
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
