﻿using TaskForAlifTech.Domain.Commons;

namespace TaskForAlifTech.Domain.Entities.Users
{
    public class Wallet : Auditable<long>
    {
        public decimal Balance { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}