using ExpenseMaster.BusinessLogic.AbstractDto;

namespace ExpenseMaster.BusinessLogic.Dto
{
    public class UserLoginDto : BaseUserDto
    {
        public string Password { get; set; } = string.Empty;
    }
}
