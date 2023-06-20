using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IIncomeService
    {
        Task<IEnumerable<IncomeItemDto>> GetAllIncomes();
        Task<IncomeItemDto> GetIncomeById(int id);
        Task<IncomeDto> CreateIncome(IncomeDto IncomeDto);
        Task<IncomeItemDto> UpdateIncome(IncomeItemDto incomeItemDto);
        Task DeleteIncome(IncomeItemDto incomeItemDto);
        Task<decimal> CalculateTotalIncomeByUserId(int userId);
        Task<IEnumerable<IncomeDto>> GetIncomesByCategory(int categoryId);
    }
}
