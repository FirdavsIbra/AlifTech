using Newtonsoft.Json;


namespace TaskOfAlifTech.Domain.Commons
{
    public abstract class Auditable
    {
        public long Id { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }
    }
}
