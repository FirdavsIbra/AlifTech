using System.Linq.Expressions;

namespace TaskForAlifTech.Data.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAllAsync(Expression<Func<T, bool>> expression = null, string include = null, bool isTracking = true);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
    }
}
