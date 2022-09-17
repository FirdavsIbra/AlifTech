using System.Linq.Expressions;

namespace TasOfAlifTech.Data.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        T Update(T entity);
        void DeleteRange(IEnumerable<T> entities);  
        IQueryable<T> Where(Expression<Func<T, bool>> expression = null, bool isTracking = true, string[]? include = null);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    }
}
