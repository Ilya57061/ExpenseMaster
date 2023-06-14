using System.Linq.Expressions;

namespace ExpenseMaster.DAL.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<IQueryable<T>> FindAllAsync();
        Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
