using TaskOfAlifTech.Domain.Entities.Users;
using TaskOfAlifTech.Domain.Entities.Transactions;

namespace TasOfAlifTech.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Wallet> Wallets { get; }
        IGenericRepository<Transaction> Transactions { get; }

        Task SaveChangesAsync();
    }
}
