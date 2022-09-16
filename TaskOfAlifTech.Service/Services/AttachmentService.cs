using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskOfAlifTech.Domain.Entities.Attachments;
using TaskOfAlifTech.Service.Helpers;
using TaskOfAlifTech.Service.Interfaces;
using TasOfAlifTech.Data.IRepositories;
using TasOfAlifTech.Data.Repositories;

namespace TaskOfAlifTech.Service.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IUnitOfWork unitOfWork;
        public AttachmentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Attachment> UploadAsync(Stream stream, string fileName)
        {
            // store to wwwroot
            fileName = Guid.NewGuid().ToString("N") + "-" + fileName;
            string filePath = Path.Combine(EnvironmentHelper.AttachmentPath, fileName);

            FileStream fileStream = File.Create(filePath);
            await stream.CopyToAsync(fileStream);

            await fileStream.FlushAsync();
            fileStream.Close();

            // store to database
            var attachment = await unitOfWork.Attachments.CreateAsync(new Attachment()
            {
                Name = Path.GetFileName(filePath),
                Path = Path.Combine(EnvironmentHelper.FilePath, Path.GetFileName(filePath)),
                CreatedAt = DateTime.UtcNow
            });

            await unitOfWork.SaveChangesAsync();

            return attachment;
        }
        public Task<bool> DeleteAsync(Expression<Func<Attachment, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<Attachment> UpdateAsync(long id, Stream stream)
        {
            throw new NotImplementedException();
        }

    }
}
