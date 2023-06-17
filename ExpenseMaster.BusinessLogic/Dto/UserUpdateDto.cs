namespace ExpenseMaster.BusinessLogic.Dto
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }
    }
}
