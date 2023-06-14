namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IBudgetRepository Budget { get; }
        Task SaveAsync();
    }
}
