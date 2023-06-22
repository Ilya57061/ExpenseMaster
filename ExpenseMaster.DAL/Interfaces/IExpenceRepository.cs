using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.DAL.Interfaces
{
    public interface IExpenceRepository : IRepositoryBase<Expense>
    {
        Task<decimal> CalculateTotalExpensesByUserId(int userId);
    }
}
