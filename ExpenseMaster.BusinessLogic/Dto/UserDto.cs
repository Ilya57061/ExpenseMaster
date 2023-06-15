namespace ExpenseMaster.BusinessLogic.Dto
{
    public class UserDto
    {
        public string Login { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public RoleDto Role { get; set; }
    }
}
