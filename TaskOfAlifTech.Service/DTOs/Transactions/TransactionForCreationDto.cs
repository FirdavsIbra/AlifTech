using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskOfAlifTech.Service.DTOs.Transactions
{
    public class TransactionForCreationDto
    {
        public long FromWalletId { get; set; }
        public long ToWalletId { get; set; }
        public decimal Amount { get; set; } 

    }
}
