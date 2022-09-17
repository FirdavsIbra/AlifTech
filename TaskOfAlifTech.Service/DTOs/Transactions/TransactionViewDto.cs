using System;
using TaskOfAlifTech.Domain.Entities.Users;

namespace TaskOfAlifTech.Service.DTOs.Transactions
{
    public class TransactionViewDto
    {
        public string From { get; set; }
        public string To { get; set; }  
        public decimal Amount { get; set; } 
        public DateTime CreatedAt { get; set; } 
    }
}
