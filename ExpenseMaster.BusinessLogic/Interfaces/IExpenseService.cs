using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseWithIdDto>> GetAllExpenses();
        Task<ExpenseWithIdDto> GetExpenseById(int id);
        Task<ExpenseDto> CreateExpense(ExpenseDto ExpenseDto);
        Task<ExpenseWithIdDto> UpdateExpense(ExpenseWithIdDto expenseWithIdDto);
        Task DeleteExpense(ExpenseWithIdDto expenseWithIdDto);
        Task<decimal> CalculateTotalExpensesByUserId(int userId);
        Task<IEnumerable<ExpenseDto>> GetExpensesByCategory(int categoryId);
    }
}
