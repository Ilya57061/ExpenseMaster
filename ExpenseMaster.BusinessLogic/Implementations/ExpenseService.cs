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

        public async Task<IEnumerable<ExpenseWithIdDto>> GetAllExpenses()
        {
            var expenses = await _repositoryWrapper.Expence.FindAllAsync();
            var expensesWithIdDto = _mapper.Map<IEnumerable<ExpenseWithIdDto>>(expenses);

            return expensesWithIdDto;
        }

        public async Task<ExpenseWithIdDto> GetExpenseById(int id)
        {
            var result = await _repositoryWrapper.Expence.FindByConditionAsync(x => x.Id == id);
            var expense = await result.FirstOrDefaultAsync();
            var expenseDto = _mapper.Map<ExpenseWithIdDto>(expense);

            return expenseDto;
        }

        public async Task<ExpenseDto> CreateExpense(ExpenseDto expenseDto)
        {
            var expense = _mapper.Map<Expense>(expenseDto);

            await _repositoryWrapper.Expence.CreateAsync(expense);
            await _repositoryWrapper.SaveAsync();

            return expenseDto;
        }

        public async Task<ExpenseWithIdDto> UpdateExpense(ExpenseWithIdDto expenseWithIdDto)
        {
            var existingExpense = await _repositoryWrapper.Expence.FindByConditionAsync(x=> x.Id == expenseWithIdDto.Id);
            if(existingExpense == null) 
            {
                throw new Exception("Expense not found");
            }

            var expense = _mapper.Map<Expense>(expenseWithIdDto);

            await _repositoryWrapper.Expence.UpdateAsync(expense);
            await _repositoryWrapper.SaveAsync();

            var updateExpense = _mapper.Map<ExpenseWithIdDto>(expense);

            return updateExpense;
        }

        public async Task DeleteExpense(ExpenseWithIdDto expenseWithIdDto)
        {
            var existingExpense = await _repositoryWrapper.Expence.FindByConditionAsync(x => x.Id == expenseWithIdDto.Id);
            var expenseToDelete = await existingExpense.FirstOrDefaultAsync();
            if (expenseToDelete != null)
            {
                await _repositoryWrapper.Expence.DeleteAsync(expenseToDelete);
                await _repositoryWrapper.SaveAsync();
            }
        }

        public async Task<IEnumerable<ExpenseDto>> GetExpensesByCategory(int categoryId)
        {
            var expenses = await _repositoryWrapper.Expence.FindByConditionAsync(x => x.CategoryId == categoryId);
            var expensesDto = _mapper.Map<IEnumerable<ExpenseDto>>(expenses);

            return expensesDto;
        }

        public async Task<decimal> CalculateTotalExpensesByUserId(int userId)
        {
            var expenses = await _repositoryWrapper.Expence.FindByConditionAsync(x => x.UserId == userId);
            decimal totalExpenses = expenses.Sum(x => x.Amount);

            return totalExpenses;
        }
    }
}
