using AutoMapper;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class FinancialGoalService : IFinancialGoalService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public FinancialGoalService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task CreateAsync(FinancialGoal financialGoal)
        {
            await _repositoryWrapper.FinancialGoal.CreateAsync(financialGoal);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task DeleteAsync(FinancialGoal financialGoal)
        {
            await _repositoryWrapper.FinancialGoal.DeleteAsync(financialGoal);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task<FinancialGoal> GetByIdAsync(int id)
        {
            var financialGoal = await _repositoryWrapper.FinancialGoal.GetByIdAsync(id);

            return financialGoal;
        }

        public async Task<IEnumerable<FinancialGoal>> GetByTargetAmountAsync(int userId)
        {
            var goals = await _repositoryWrapper.FinancialGoal.GetByUserIdAsync(userId);

            var goalsReachedTarget = goals.Where(g => g.CurrentAmount >= g.TargetAmount);

            return goalsReachedTarget;
        }

        public async Task<IEnumerable<FinancialGoal>> GetByUserIdAsync(int userId)
        {
            var financailGoals = await _repositoryWrapper.FinancialGoal.GetByUserIdAsync(userId);

            return financailGoals;
        }

        public async Task<decimal> GetTotalProgressAsync(int userId)
        {
            var goals = await _repositoryWrapper.FinancialGoal.GetByUserIdAsync(userId);

            decimal totalProgress = goals.Sum(g => g.CurrentAmount);

            return totalProgress;
        }

        public async Task UpdateAsync(FinancialGoal financialGoal)
        {
            await _repositoryWrapper.FinancialGoal.UpdateAsync(financialGoal);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task UpdateCurrentAmountAsync(int goalId, decimal currentAmount)
        {
            var financialGoal = await _repositoryWrapper.FinancialGoal.GetByIdAsync(goalId);

            if (financialGoal == null)
            {
                throw new ArgumentException("Invalid goalId");
            }

            financialGoal.CurrentAmount = currentAmount;

            _repositoryWrapper.FinancialGoal.UpdateAsync(financialGoal);
            await _repositoryWrapper.SaveAsync();
        }
    }
}
