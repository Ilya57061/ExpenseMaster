using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class ExpenseService : IExpenseService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ExpenseService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Expense>> GetAllExpenses()
        {
            return await _repositoryWrapper.Expence.FindAllAsync();
        }

        public async Task<Expense> GetExpenseById(int id)
        {
            var result = await _repositoryWrapper.Expence.FindByConditionAsync(x => x.Id == id);
            var expense = await result.FirstOrDefaultAsync();

            return expense;
        }

        public async Task<Expense> CreateExpense(CreateExpenseDto createExpenseDto)
        {
            var expense = _mapper.Map<Expense>(createExpenseDto);

            await _repositoryWrapper.Expence.CreateAsync(expense);
            await _repositoryWrapper.SaveAsync();

            return expense;
        }

        public async Task<Expense> UpdateExpense(UpdateExpenseDto updateExpenseDto)
        {
            var existingExpense = await _repositoryWrapper.Expence.FindByConditionAsync(x=> x.Id == updateExpenseDto.Id);
            if(existingExpense == null) 
            {
                throw new Exception("Expense not found");
            }

            var expense = _mapper.Map<Expense>(updateExpenseDto);

            await _repositoryWrapper.Expence.UpdateAsync(expense);
            await _repositoryWrapper.SaveAsync();

            return expense;
        }

        public async Task DeleteExpense(Expense expense)
        {
            var existingExpense = await _repositoryWrapper.Expence.FindByConditionAsync(x => x.Id == expense.Id);
            var expenseToDelete = await existingExpense.FirstOrDefaultAsync();
            if (expenseToDelete != null)
            {
                await _repositoryWrapper.Expence.DeleteAsync(expenseToDelete);
                await _repositoryWrapper.SaveAsync();
            }
        }

        public async Task<IEnumerable<Expense>> GetExpensesByCategory(int categoryId)
        {
            var expense = await _repositoryWrapper.Expence.FindByConditionAsync(x => x.CategoryId == categoryId);

            return expense.ToList();
        }

        public async Task<decimal> CalculateTotalExpensesByUserId(int userId)
        {
            var expenses = await _repositoryWrapper.Expence.FindByConditionAsync(x => x.UserId == userId);
            decimal totalExpenses = expenses.Sum(x => x.Amount);

            return totalExpenses;
        }
    }
}
