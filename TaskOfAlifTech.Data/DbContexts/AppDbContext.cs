using Microsoft.EntityFrameworkCore;
using TaskOfAlifTech.Domain.Entities.Attachments;
using TaskOfAlifTech.Domain.Entities.Users;

namespace TasOfAlifTech.Data.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
    }
}
