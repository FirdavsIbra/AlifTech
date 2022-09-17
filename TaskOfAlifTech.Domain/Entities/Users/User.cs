using Newtonsoft.Json;
using TaskOfAlifTech.Domain.Commons;
using TaskOfAlifTech.Domain.Enums;

namespace TaskOfAlifTech.Domain.Entities.Users
{
    public class User : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonIgnore]
        public string Login { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        [JsonIgnore]
        public bool IsIdentified { get; set; }

        [JsonIgnore]
        public ItemState State { get; set; } = ItemState.Created;
    }
}
