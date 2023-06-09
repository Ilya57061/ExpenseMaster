﻿using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class BudgetService : IBudgetService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public BudgetService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<ReturnBudgetDto> GetByIdAsync(int id)
        {
            var budget = await _repositoryWrapper.Budget.GetByIdAsync(id);
            if (budget == null)
            {
                throw new InvalidOperationException($"Budget with id - {id} was not found");
            }
            var budgetDto = _mapper.Map<ReturnBudgetDto>(budget);

            return budgetDto;
        }

        public async Task CreateAsync(CreateBudgetDto budgetDto)
        {
            var budget = _mapper.Map<Budget>(budgetDto);
            await _repositoryWrapper.Budget.CreateAsync(budget);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task UpdateAsync(UpdateBudgetDto budgetDto)
        {
            var budget = _mapper.Map<Budget>(budgetDto);
            await _repositoryWrapper.Budget.UpdateAsync(budget);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var budget = await _repositoryWrapper.Budget.GetByIdAsync(id);
            if (budget == null)
            {
                throw new InvalidOperationException($"Budget with id - {id} was not found");
            }
            await _repositoryWrapper.Budget.DeleteAsync(budget);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task<IEnumerable<ReturnBudgetDto>> GetByUserIdAsync(int userId)
        {
            var budgets = await _repositoryWrapper.Budget.GetBudgetsByUserIdAsync(userId);
            if (budgets == null)
            {
                throw new InvalidOperationException($"Budgets with UserId - {userId} was not found");
            }
            var budgetDto = _mapper.Map<IEnumerable<ReturnBudgetDto>>(budgets);

            return budgetDto;
        }

        public async Task<IEnumerable<ReturnBudgetDto>> GetByCategoryIdAsync(int userId, int categoryId)
        {
            var budgets = await _repositoryWrapper.Budget.GetBudgetByCategoryIdAsync(userId, categoryId);
            var budgetDto = _mapper.Map<IEnumerable<ReturnBudgetDto>>(budgets);

            return budgetDto;
        }

        public async Task<IEnumerable<ReturnBudgetDto>> GetBudgetsExceedingThresholdAsync(int userId)
        {
            var budgets = await _repositoryWrapper.Budget.GetBudgetsExceedingThresholdAsync(userId);
            var budgetDto = _mapper.Map<IEnumerable<ReturnBudgetDto>>(budgets);

            return budgetDto;
        }

        public async Task UpdateWarningThresholdAsync(int budgetId, decimal warningThreshold)
        {
            var budget = await _repositoryWrapper.Budget.GetByIdAsync(budgetId);
            budget.WarningThreshold = warningThreshold;
            await _repositoryWrapper.Budget.UpdateAsync(budget);
            await _repositoryWrapper.SaveAsync();
        }
    }
}
