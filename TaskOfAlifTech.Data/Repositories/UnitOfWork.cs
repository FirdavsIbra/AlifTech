using TaskOfAlifTech.Domain.Entities.Attachments;
using TaskOfAlifTech.Domain.Entities.Users;
using TasOfAlifTech.Data.DbContexts;
using TasOfAlifTech.Data.IRepositories;

namespace TasOfAlifTech.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Dbsets of databse
        /// </summary>
        public IGenericRepository<User> Users { get; }
        public IGenericRepository<Wallet> Wallets { get; }
        public IGenericRepository<Attachment> Attachments { get; }

        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(dbContext);
        }

        /// <summary>
        /// Save changes in ORM
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
