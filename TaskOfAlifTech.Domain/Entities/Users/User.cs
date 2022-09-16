using TaskForAlifTech.Domain.Commons;
using TaskForAlifTech.Domain.Enums;

namespace TaskForAlifTech.Domain.Entities.Users
{
    public class User : Auditable<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public UserStatus Status { get; set; }
        public ItemState State { get; set; }
    }
}
