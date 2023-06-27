using ExpenseMaster.BusinessLogic.Dto;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IIncomeService
    {
        Task<IEnumerable<IncomeItemDto>> GetAllIncomes();
        Task<IncomeItemDto> GetIncomeById(int id);
        Task<IncomeItemDto> CreateIncome(IncomeDto IncomeDto);
        Task<IncomeItemDto> UpdateIncome(IncomeItemDto incomeItemDto);
        Task DeleteIncome(IncomeItemDto incomeItemDto);
        Task<decimal> CalculateTotalIncomeByUserId(int userId);
        Task<IEnumerable<IncomeDto>> GetIncomesByCategory(int categoryId);
    }
}
