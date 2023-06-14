using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.DAL.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetUserByLoginAsync(string login);
    }
}
