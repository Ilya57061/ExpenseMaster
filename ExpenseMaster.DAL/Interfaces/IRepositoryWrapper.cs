using ExpenseMaster.DAL.Interfaces;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IIncomeRepository Income { get; }
        IExpenceRepository Expence { get; }
        IBudgetRepository Budget { get; }
        Task SaveAsync();
    }
}
