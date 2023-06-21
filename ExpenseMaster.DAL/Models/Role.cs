using ExpenseMaster.DAL.Constants;

namespace ExpenseMaster.DAL.Models
{
    public class Role
    {
        public int Id { get; set; }
        public RoleType Name { get; set; }
        public List<User>? Users { get; set; }
    }
}
