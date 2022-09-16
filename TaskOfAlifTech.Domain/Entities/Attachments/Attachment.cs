using TaskForAlifTech.Domain.Commons;

namespace TaskForAlifTech.Domain.Entities.Attachments
{
    public class Attachment : Auditable<long>
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
