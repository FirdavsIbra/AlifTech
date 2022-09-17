using Microsoft.EntityFrameworkCore;
using TaskOfAlifTech.Domain.Entities.Users;
using TaskOfAlifTech.Domain.Entities.Transactions;

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
        public virtual DbSet<Transaction> Transactions { get; set; }
    }   
}
