using TaskOfAlifTech.Domain.Entities.Users;

namespace TaskOfAlifTech.Service.DTOs.Users
{
    public class UserViewDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public bool IsIdentified { get; set; }
        public IEnumerable<Wallet>? Wallets { get; set; } 
    }
}
