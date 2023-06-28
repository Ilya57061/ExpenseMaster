using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.BusinessLogic.Validators.Interfaces;
using ExpenseMaster.DAL.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class ExpenseService : IExpenseService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly INotificationService _notificationService;
        private readonly IValidatorWrapper _validatorWrapper;

        public ExpenseService(IRepositoryWrapper repositoryWrapper, IMapper mapper, INotificationService notificationService, IValidatorWrapper validatorWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _notificationService = notificationService;
            _validatorWrapper = validatorWrapper;
        }

        public async Task<IEnumerable<ExpenseItemDto>> GetAllExpenses()
        {
            var expenses = await _repositoryWrapper.Expence.FindAllAsync();
            var expensesItemDto = _mapper.Map<IEnumerable<ExpenseItemDto>>(expenses);

            return expensesItemDto;
        }

        public async Task<ExpenseItemDto> GetExpenseById(int id)
        {
            var result = await _repositoryWrapper.Expence.FindByConditionAsync(x => x.Id == id);
            var expense = await result.FirstOrDefaultAsync();
            var expenseItemDto = _mapper.Map<ExpenseItemDto>(expense);

            if (expenseItemDto == null)
            {
                throw new InvalidOperationException($"Expense with id - {id} not found");
            }

            return expenseItemDto;
        }

        public async Task<ExpenseItemDto> CreateExpense(ExpenseDto expenseDto)
        {
            ValidateDto(expenseDto);

            var expense = _mapper.Map<Expense>(expenseDto);

            await _repositoryWrapper.Expence.CreateAsync(expense);
            await _repositoryWrapper.SaveAsync();

            var createdExpense = _mapper.Map<ExpenseItemDto>(expense);

            await CheckExpenseExceeded(expense.UserId);

            return createdExpense;
        }

        public async Task CheckExpenseExceeded(int userId)
        {
            var expensesTotal = await _repositoryWrapper.Expence.CalculateTotalExpensesByUserId(userId);

            var incomesTotal = await _repositoryWrapper.Income.CalculateTotalIncomeByUserId(userId);

            if (expensesTotal > incomesTotal)   
            {
                var existingUser = await _repositoryWrapper.User.FindByConditionAsync(x=> x.Id == userId);
                var user = existingUser.FirstOrDefault();

                await _notificationService.SendExpenseExceededNotification(user.Email);
            }
        }

        public async Task<ExpenseItemDto> UpdateExpense(ExpenseItemDto expenseItemDto)
        {
            ValidateDto(expenseItemDto);

            var existingExpense = await _repositoryWrapper.Expence.FindByConditionAsync(x=> x.Id == expenseItemDto.Id);

            if(existingExpense == null) 
            {
                throw new InvalidOperationException($"Expense with id - {expenseItemDto.Id} not found");
            }

            var expense = _mapper.Map<Expense>(expenseItemDto);

            await _repositoryWrapper.Expence.UpdateAsync(expense);
            await _repositoryWrapper.SaveAsync();

            var updateExpense = _mapper.Map<ExpenseItemDto>(expense);

            return updateExpense;
        }

        public async Task DeleteExpense(ExpenseItemDto expenseItemDto)
        {
            ValidateDto(expenseItemDto);

            var existingExpense = await _repositoryWrapper.Expence.FindByConditionAsync(x => x.Id == expenseItemDto.Id);
            var expenseToDelete = await existingExpense.FirstOrDefaultAsync();

            if (expenseToDelete == null)
            {
                throw new InvalidOperationException($"Expense with id - {expenseItemDto.Id} not found");
            }

            await _repositoryWrapper.Expence.DeleteAsync(expenseToDelete);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task<IEnumerable<ExpenseDto>> GetExpensesByCategory(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(categoryId), "Category ID must be a positive integer.");
            }

            var expenses = await _repositoryWrapper.Expence.FindByConditionAsync(x => x.CategoryId == categoryId);
            var expensesDto = _mapper.Map<IEnumerable<ExpenseDto>>(expenses);

            return expensesDto;
        }

        public async Task<decimal> CalculateTotalExpensesByUserId(int userId)
        {
            var totalExpenses = await _repositoryWrapper.Expence.CalculateTotalExpensesByUserId(userId);

            return totalExpenses;
        }

        private void ValidateDto<T>(T dto)
        {
            ValidationResult validationResult = _validatorWrapper.Validate(dto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
    }
}
