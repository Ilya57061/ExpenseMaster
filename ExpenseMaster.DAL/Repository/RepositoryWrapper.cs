using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.DatabaseContext;
using ExpenseMaster.DAL.Interfaces;

namespace ExpenseMaster.DAL.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDatabaseContext _appContext;
        private IUserRepository _user;
        private IIncomeRepository _income;
        private IExpenceRepository _expence;
        private IBudgetRepository _budget;
        private IFinancialGoalRepository _financialGoal;

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_appContext);
                }

                return _user;
            }
        }

        public IIncomeRepository Income
        {
            get
            {
                if(_income == null)
                {
                    _income = new IncomeRepository(_appContext);
                }

                return _income;
            }
        }
        public IExpenceRepository Expence
        {
            get
            {
                if(_expence == null)
                {
                    _expence = new ExpenceRepository(_appContext);
                }

                return _expence;
            }
        }


        public IBudgetRepository Budget
        {
            get
            {
                if (_budget == null)
                {
                    _budget = new BudgetRepository(_appContext);
                }
                return _budget;
            }
        }

        public IFinancialGoalRepository FinancialGoal
        {
            get
            {
                if (_financialGoal == null)
                {
                    _financialGoal = new FinancialGoalRepository(_appContext);
                }

                return _financialGoal;

            }
        }

        public RepositoryWrapper(ApplicationDatabaseContext appContext)
        {
            _appContext = appContext;
        }

        public async Task SaveAsync()
        {
            await _appContext.SaveChangesAsync();
        }
    }
}
