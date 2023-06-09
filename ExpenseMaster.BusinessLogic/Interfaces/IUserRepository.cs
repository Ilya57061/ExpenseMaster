using ExpenseMaster.Model.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByLoginAsync(string login);
    }
}
