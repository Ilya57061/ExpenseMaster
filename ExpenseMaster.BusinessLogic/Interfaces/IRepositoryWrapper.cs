namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IIncomeRepository Income { get; }
        Task SaveAsync();
    }
}
