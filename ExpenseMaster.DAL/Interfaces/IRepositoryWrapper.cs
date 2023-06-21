using ExpenseMaster.DAL.Interfaces;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IIncomeRepository Income { get; }
        IExpenceRepository Expence { get; }
        IFinancialGoalRepository FinancialGoal { get; }
        Task SaveAsync();
    }
}
