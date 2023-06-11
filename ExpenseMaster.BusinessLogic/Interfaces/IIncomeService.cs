using ExpenseMaster.Model.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IIncomeService
    {
        Task<decimal> CalculateTotalIncomeByUserId(int userId);
        Task<IEnumerable<Income>> GetIncomesByCategory(int categoryId);
    }
}
