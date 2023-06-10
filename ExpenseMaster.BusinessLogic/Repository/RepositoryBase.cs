using ExpenseMaster.BusinessLogic.Interfaces;
using System.Linq.Expressions;

namespace ExpenseMaster.BusinessLogic.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public ApplicationDatabaseContext AppContext { get; set; }

        public RepositoryBase(ApplicationDatabaseContext appContext)
        {
            AppContext = appContext;
        }

        public async Task<IQueryable<T>> FindAllAsync()
        {
            return await AppContext.Set<T>().AsNoTracking();
        }

        public async Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await AppContext.Set<T>().Where(expression).AsNoTracking();
        }

        public async Task CreateAsync(T entity)
        {
            return await AppContext.Set<T>().Add(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            return await AppContext.Set<T>().Update(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            return await AppContext.Set<T>().Remove(entity);
        }
    }
}
