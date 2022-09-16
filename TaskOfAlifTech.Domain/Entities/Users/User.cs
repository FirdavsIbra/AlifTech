using TaskOfAlifTech.Domain.Commons;
using TaskOfAlifTech.Domain.Enums;

namespace TaskOfAlifTech.Domain.Entities.Users
{
    public class User : Auditable<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public UserStatus? Status { get; set; } = UserStatus.Identified;
        public ItemState? State { get; set; } = ItemState.Created;
    }
}
