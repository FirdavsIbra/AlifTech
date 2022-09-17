using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskOfAlifTech.Domain.Configurations;
using TaskOfAlifTech.Domain.Entities.Users;
using TaskOfAlifTech.Service.DTOs.Users;

namespace TaskOfAlifTech.Service.Interfaces
{
    public interface IUserService
    {
        Task<User> AddAsync(UserForCreationDto dto);
        Task<User> UpdateAsync(long id, UserForCreationDto dto);
        Task<bool> DeleteAsync(Expression<Func<User, bool>> expression);
        Task<UserViewDto> GetAsync(Expression<Func<User, bool>> expression);
        Task<IEnumerable<User>> GetAllAsync(PaginationParams? @params, Expression<Func<User, bool>>? expression = null);
    }
}
