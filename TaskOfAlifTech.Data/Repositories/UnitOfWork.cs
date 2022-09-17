using TaskOfAlifTech.Domain.Entities.Users;
using TaskOfAlifTech.Domain.Entities.Transactions;
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
            Users = new GenericRepository<User>(dbContext);
            Wallets = new GenericRepository<Wallet>(dbContext);
            Transactions = new GenericRepository<Transaction>(dbContext);
        }

        /// <summary>
        /// Dbsets of databse
        /// </summary>
        public IGenericRepository<User> Users { get; }
        public IGenericRepository<Wallet> Wallets { get; }
        public IGenericRepository<Transaction> Transactions { get; }

        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose() => GC.SuppressFinalize(dbContext);

        /// <summary>
        /// Save changes in ORM
        /// </summary>
        /// <returns></returns>
        public Task SaveChangesAsync() => dbContext.SaveChangesAsync();
    }
}
