using System.ComponentModel.DataAnnotations.Schema;
using TaskOfAlifTech.Domain.Commons;

namespace TaskOfAlifTech.Domain.Entities.Users
{
    public class Wallet : Auditable
    {
        public decimal Balance { get; set; }
        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]    
        public User? User { get; set; }
    }
}
