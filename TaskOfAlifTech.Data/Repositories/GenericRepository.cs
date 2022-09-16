using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskForAlifTech.Data.DbContexts;
using TaskForAlifTech.Data.IRepositories;

namespace TaskForAlifTech.Data.Repositories
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

        public async Task<T> CreateAsync(T entity)
        {
            var entry = await _dbSet.AddAsync(entity);

            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(expression);

            if (entity == null)
                return false;

            _dbSet.Remove(entity);

            return true;
        }

        public IQueryable<T> GetAllAsync(Expression<Func<T, bool>> expression = null, string include = null, bool isTracking = true)
        {
            IQueryable<T> query = expression is null ? _dbSet : _dbSet.Where(expression);

            if (!string.IsNullOrEmpty(include))
                query = query.Include(include);

            if (!isTracking)
                query = query.AsNoTracking();

            return query;
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefaultAsync(expression);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            return _dbSet.Update(entity).Entity;
        }
    }
}
