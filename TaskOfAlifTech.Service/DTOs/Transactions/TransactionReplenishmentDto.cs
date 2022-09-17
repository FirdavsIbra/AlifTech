using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskOfAlifTech.Service.DTOs.Transactions
{
    public class TransactionReplenishmentDto
    {
        public long TotalCount { get; set; }
        public decimal TotalAmount { get; set; } 
        public IEnumerable<TransactionViewDto>? Data { get; set; }
    }
}
