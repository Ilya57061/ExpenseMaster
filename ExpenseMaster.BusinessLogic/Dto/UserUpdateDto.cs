using ExpenseMaster.BusinessLogic.AbstractDto;

namespace ExpenseMaster.BusinessLogic.Dto
{
    public class UserUpdateDto : BaseUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }
    }
}
