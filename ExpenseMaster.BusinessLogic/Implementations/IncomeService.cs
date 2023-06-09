﻿using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class IncomeService : IIncomeService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public IncomeService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IncomeItemDto>> GetAllIncomes()
        {
            var incomes =  await _repositoryWrapper.Income.FindAllAsync();
            var incomesItemDto = _mapper.Map<IEnumerable<IncomeItemDto>>(incomes);

            return incomesItemDto;
        }

        public async Task<IncomeItemDto> GetIncomeById(int id)
        {
            var result = await _repositoryWrapper.Income.FindByConditionAsync(x => x.Id == id);
            var income = result.FirstOrDefault();
            if (income == null)
            {
                throw new InvalidOperationException($"Income with id - {id} not found");
            }
            var incomeItemDto = _mapper.Map<IncomeItemDto>(income);

            return incomeItemDto;
        }

        public async Task<IncomeItemDto> CreateIncome(IncomeDto incomeDto)
        {
            if (incomeDto == null)
            {
                throw new ArgumentNullException(nameof(incomeDto));
            }

            var income = _mapper.Map<Income>(incomeDto);

            await _repositoryWrapper.Income.CreateAsync(income);
            await _repositoryWrapper.SaveAsync();

            var createdIncome = _mapper.Map<IncomeItemDto>(income);

            return createdIncome;
        }

        public async Task<IncomeItemDto> UpdateIncome(IncomeItemDto incomeItemDto)
        {
            if (incomeItemDto == null)
            {
                throw new ArgumentNullException(nameof(incomeItemDto));
            }

            var existingIncome = await _repositoryWrapper.Income.FindByConditionAsync(x => x.Id == incomeItemDto.Id);

            if (existingIncome == null)
            {
                throw new InvalidOperationException($"Income with id - {incomeItemDto.Id} not found");
            }

            var income = _mapper.Map<Income>(incomeItemDto);

            await _repositoryWrapper.Income.UpdateAsync(income);
            await _repositoryWrapper.SaveAsync();

            var updateIncome = _mapper.Map<IncomeItemDto>(income);

            return updateIncome;
        }

        public async Task DeleteIncome(IncomeItemDto incomeItemDto)
        {
            if (incomeItemDto == null)
            {
                throw new ArgumentNullException(nameof(incomeItemDto));
            }

            var existingIncome = await _repositoryWrapper.Income.FindByConditionAsync(x => x.Id == incomeItemDto.Id);
            var incomeToDelete = existingIncome.FirstOrDefault();

            if (incomeToDelete != null)
            {
                await _repositoryWrapper.Income.DeleteAsync(incomeToDelete);
                await _repositoryWrapper.SaveAsync();
            }
        }

        public async Task<IEnumerable<IncomeDto>> GetIncomesByCategory(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(categoryId), "Category ID must be a positive integer.");
            }

            var incomes = await _repositoryWrapper.Income.FindByConditionAsync(x => x.CategoryId == categoryId);
            var incomesDto = _mapper.Map<IEnumerable<IncomeDto>>(incomes);

            return incomesDto;
        }

        public async Task<decimal> CalculateTotalIncomeByUserId(int userId)
        {
            var totalIncomes = await _repositoryWrapper.Income.CalculateTotalIncomeByUserId(userId);

            return totalIncomes;
        }
    }
}
