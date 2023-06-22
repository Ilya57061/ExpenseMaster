using ExpenseMaster.BusinessLogic.Dto;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseItemDto>> GetAllExpenses();
        Task<ExpenseItemDto> GetExpenseById(int id);
        Task<ExpenseItemDto> CreateExpense(ExpenseDto ExpenseDto);
        Task<ExpenseItemDto> UpdateExpense(ExpenseItemDto expenseItemDto);
        Task DeleteExpense(ExpenseItemDto expenseItemDto);
        Task<decimal> CalculateTotalExpensesByUserId(int userId);
        Task<IEnumerable<ExpenseDto>> GetExpensesByCategory(int categoryId);
    }
}
