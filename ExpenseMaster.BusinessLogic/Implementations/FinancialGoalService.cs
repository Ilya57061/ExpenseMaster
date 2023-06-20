using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class FinancialGoalService : IFinancialGoalService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public FinancialGoalService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateFinancialGoalDto financialGoal)
        {
            var goal = _mapper.Map<FinancialGoal>(financialGoal);

            await _repositoryWrapper.FinancialGoal.CreateAsync(goal);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var financialGoal = await _repositoryWrapper.FinancialGoal.GetByIdAsync(id);

            if (financialGoal == null)
            {
                throw new InvalidOperationException($"FinancialGoal with id - {id} was not found");
            }

            await _repositoryWrapper.FinancialGoal.DeleteAsync(financialGoal);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task<ReturnFinancialGoalDto> GetByIdAsync(int id)
        {
            var financialGoal = await _repositoryWrapper.FinancialGoal.GetByIdAsync(id);

            if (financialGoal == null)
            {
                throw new InvalidOperationException($"FinancialGoal with UserId - {id} was not found");
            }

            var financialGoalDto = _mapper.Map<ReturnFinancialGoalDto>(financialGoal);

            return financialGoalDto;
        }

        public async Task<IEnumerable<ReturnFinancialGoalDto>> GetByTargetAmountAsync(int userId)
        {
            var goals = await _repositoryWrapper.FinancialGoal.GetByUserIdAsync(userId);
            var goalsReachedTarget = goals.Where(g => g.CurrentAmount >= g.TargetAmount);
            var financialGoalDto = _mapper.Map<IEnumerable<ReturnFinancialGoalDto>>(goalsReachedTarget);

            return financialGoalDto;
        }

        public async Task<IEnumerable<ReturnFinancialGoalDto>> GetByUserIdAsync(int userId)
        {
            var financialGoals = await _repositoryWrapper.FinancialGoal.GetByUserIdAsync(userId);
            var financialGoalDto = _mapper.Map<IEnumerable<ReturnFinancialGoalDto>>(financialGoals);

            return financialGoalDto;
        }

        public async Task<decimal> GetTotalProgressAsync(int userId)
        {
            var goals = await _repositoryWrapper.FinancialGoal.GetByUserIdAsync(userId);
            decimal totalProgress = goals.Sum(g => g.CurrentAmount);

            return totalProgress;
        }

        public async Task UpdateAsync(UpdateFinancialGoalDto financialGoal)
        {
            var goal = _mapper.Map<FinancialGoal>(financialGoal);

            await _repositoryWrapper.FinancialGoal.UpdateAsync(goal);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task UpdateCurrentAmountAsync(int goalId, decimal currentAmount)
        {
            var financialGoal = await _repositoryWrapper.FinancialGoal.GetByIdAsync(goalId);

            if (financialGoal == null)
            {
                throw new InvalidOperationException($"FinancialGoal with Id - {goalId} was not found");
            }

            financialGoal.CurrentAmount = currentAmount;

            await _repositoryWrapper.FinancialGoal.UpdateAsync(financialGoal);
            await _repositoryWrapper.SaveAsync();
        }
    }
}
