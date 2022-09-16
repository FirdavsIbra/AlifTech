using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskOfAlifTech.Domain.Entities.Attachments;

namespace TaskOfAlifTech.Service.Interfaces
{
    public interface IAttachmentService
    {
        Task<Attachment> UploadAsync(Stream stream, string fileName);
        Task<Attachment> UpdateAsync(long id, Stream stream);
        Task<bool> DeleteAsync(Expression<Func<Attachment, bool>> expression);
    }
}
