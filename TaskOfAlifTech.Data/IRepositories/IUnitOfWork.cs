using TaskOfAlifTech.Domain.Entities.Attachments;
using TaskOfAlifTech.Domain.Entities.Users;

namespace TasOfAlifTech.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Wallet> Wallets { get; }
        IGenericRepository<Attachment> Attachments { get; }

        Task SaveChangesAsync();
    }
}
