using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetAllExpenses();
        Task<Expense> GetExpenseById(int id);
        Task<Expense> CreateExpense(CreateExpenseDto expense);
        Task<Expense> UpdateExpense(Expense expense);
        Task DeleteExpense(Expense expense);
        Task<decimal> CalculateTotalExpensesByUserId(int userId);
        Task<IEnumerable<Expense>> GetExpensesByCategory(int categoryId);
    }
}
