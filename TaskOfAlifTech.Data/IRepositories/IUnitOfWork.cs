using System.Net.Mail;
using TaskForAlifTech.Domain.Entities.Users;

namespace TaskForAlifTech.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Wallet> Wallets { get; }
        IGenericRepository<Attachment> Attachments { get; }

        Task SaveChangesAsync();
    }
}
