using ExpenseMaster.Model.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IIncomeService
    {
        Task<IEnumerable<Income>> GetAllIncomes();
        Task<Income> GetIncomeById(int id);
        Task CreateIncome(Income income);
        Task UpdateIncome(Income income);
        Task DeleteIncome(Income income);
        Task<decimal> CalculateTotalIncomeByUserId(int userId);
        Task<IEnumerable<Income>> GetIncomesByCategory(int categoryId);
    }
}
