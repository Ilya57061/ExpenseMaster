using ExpenseMaster.BusinessLogic.AbstractDto;

namespace ExpenseMaster.BusinessLogic.Dto
{
    public class ProfileDto : BaseUserDto
    {
        public string Email { get; set; } = string.Empty;
    }
}
