using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IIncomeService
    {
        Task<IEnumerable<Income>> GetAllIncomes();
        Task<Income> GetIncomeById(int id);
        Task<Income> CreateIncome(IncomeDto createIncomeDto);
        Task<Income> UpdateIncome(IncomeWithIdDto updateIncomeDto);
        Task DeleteIncome(Income income);
        Task<decimal> CalculateTotalIncomeByUserId(int userId);
        Task<IEnumerable<Income>> GetIncomesByCategory(int categoryId);
    }
}
