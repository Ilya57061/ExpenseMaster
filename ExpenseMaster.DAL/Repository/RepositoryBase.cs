using ExpenseMaster.DAL.DatabaseContext;
using ExpenseMaster.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExpenseMaster.DAL.Repository
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
            return await Task.FromResult(AppContext.Set<T>().AsNoTracking());
        }

        public async Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.FromResult(AppContext.Set<T>().Where(expression).AsNoTracking());
        }

        public async Task CreateAsync(T entity)
        {
            await Task.FromResult(AppContext.Set<T>().Add(entity));
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.FromResult(AppContext.Set<T>().Update(entity));
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.FromResult(AppContext.Set<T>().Remove(entity));
        }
    }
}
