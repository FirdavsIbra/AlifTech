using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TasOfAlifTech.Data.DbContexts;
using TasOfAlifTech.Data.IRepositories;

namespace TasOfAlifTech.Data.Repositories
{
#pragma warning disable
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _dbContext;
        protected DbSet<T> _dbSet;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
            => _dbSet.AnyAsync(expression);
        

        public async Task<T> CreateAsync(T entity)
        {
            var entry = await _dbSet.AddAsync(entity);

            return entry.Entity;
        }

        public void DeleteRange(IEnumerable<T> entities)
            => _dbSet.RemoveRange(entities);
        
        public Task<T> GetAsync(Expression<Func<T, bool>> expression) 
            => _dbSet.FirstOrDefaultAsync(expression);

        public T Update(T entity) 
            => _dbSet.Update(entity).Entity;

        public IQueryable<T> Where(Expression<Func<T, bool>> expression = null, bool isTracking = true, string[]? includes = null)
        {
            IQueryable<T> query = expression is null ? _dbSet : _dbSet.Where(expression);

            if (includes is not null)
                foreach (var include in includes)
                    query = query.Include(include);

            if (!isTracking)
                query = query.AsNoTracking();

            return query;
        }
    }
}
