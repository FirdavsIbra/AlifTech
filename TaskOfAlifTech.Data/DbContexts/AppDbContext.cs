using Microsoft.EntityFrameworkCore;
using TaskForAlifTech.Domain.Entities.Attachments;
using TaskForAlifTech.Domain.Entities.Users;

namespace TaskForAlifTech.Data.DbContexts
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
