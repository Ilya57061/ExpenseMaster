namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        Task SaveAsync();
    }
}
