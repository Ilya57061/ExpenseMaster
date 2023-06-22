using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.DAL.Interfaces
{
    public interface IIncomeRepository : IRepositoryBase<Income>
    {
        Task<decimal> CalculateTotalIncomeByUserId(int userId);
    }
}
