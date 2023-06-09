﻿using AutoMapper;
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
        private readonly INotificationService _notificationService;

        public ExpenseService(IRepositoryWrapper repositoryWrapper, IMapper mapper, INotificationService notificationService)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _notificationService = notificationService;
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

            if (expense == null)
            {
                throw new InvalidOperationException($"Expense with id - {id} not found");
            }

            var expenseItemDto = _mapper.Map<ExpenseItemDto>(expense);

            if (expenseItemDto == null)
            {
                throw new InvalidOperationException($"Expense with id - {id} not found");
            }

            return expenseItemDto;
        }

        public async Task<ExpenseItemDto> CreateExpense(ExpenseDto expenseDto)
        {
            if (expenseDto == null)
            {
                throw new ArgumentNullException(nameof(expenseDto));
            }

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
            if (expenseItemDto == null)
            {
                throw new ArgumentNullException(nameof(expenseItemDto));
            }

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
            if (expenseItemDto == null)
            {
                throw new ArgumentNullException(nameof(expenseItemDto));
            }

            var existingExpense = await _repositoryWrapper.Expence.FindByConditionAsync(x => x.Id == expenseItemDto.Id);
            var expenseToDelete = existingExpense.FirstOrDefault();

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
    }
}
