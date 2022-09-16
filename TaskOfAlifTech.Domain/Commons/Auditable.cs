namespace TaskForAlifTech.Domain.Commons
{
    public class Auditable<T>
    {
        public T Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public T CreatedBy { get; set; }
        public T UpdatedBy { get; set; }
        public T DeletedBy { get; set; }

    }
}
