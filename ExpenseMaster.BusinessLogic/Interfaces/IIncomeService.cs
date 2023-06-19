using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IIncomeService
    {
        Task<IEnumerable<IncomeWithIdDto>> GetAllIncomes();
        Task<IncomeWithIdDto> GetIncomeById(int id);
        Task<IncomeDto> CreateIncome(IncomeDto IncomeDto);
        Task<IncomeWithIdDto> UpdateIncome(IncomeWithIdDto incomeWithIdDto);
        Task DeleteIncome(IncomeWithIdDto incomeWithIdDto);
        Task<decimal> CalculateTotalIncomeByUserId(int userId);
        Task<IEnumerable<IncomeDto>> GetIncomesByCategory(int categoryId);
    }
}
