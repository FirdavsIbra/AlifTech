using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOfAlifTech.Domain.Commons;
using TaskOfAlifTech.Domain.Entities.Users;

namespace TaskOfAlifTech.Domain.Entities.Transactions
{
    public class Transaction : Auditable
    {
        public long? FromWalletId { get; set; }
        public long ToWalletId { get; set; }
        public decimal Amount { get; set; }
        public string? From { get; set; }

        [ForeignKey(nameof(FromWalletId))]
        public Wallet? FromWallet { get; set; }

        [ForeignKey(nameof(ToWalletId))]
        public Wallet? ToWallet { get; set; }
    }
}
