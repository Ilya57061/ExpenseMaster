namespace ExpenseMaster.Common.Dto
{
    public class UserDto
    {
        public string Login { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AuthorizationHeader { get; set; } = string.Empty;
    }
}
