using AutoMapper;
using System.Linq.Expressions;
using TaskOfAlifTech.Domain.Configurations;
using TaskOfAlifTech.Domain.Entities.Users;
using TaskOfAlifTech.Domain.Enums;
using TaskOfAlifTech.Service.DTOs.Users;
using TaskOfAlifTech.Service.Exceptions;
using TaskOfAlifTech.Service.Extensions;
using TaskOfAlifTech.Service.Interfaces;
using TasOfAlifTech.Data.IRepositories;

namespace TaskOfAlifTech.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<User> AddAsync(UserForCreationDto dto)
        {
            // check for exist
            var anyUser = await unitOfWork.Users.AnyAsync(u => 
                u.Login == dto.Login && u.State != ItemState.Deleted);
            if (anyUser)
                throw new AppException(400, "User already exist!");

            dto.Password = StringExtension.PasswordHash(dto.Password);

            // map entity
            var newUser = await unitOfWork.Users.CreateAsync(mapper.Map<User>(dto));
            await unitOfWork.SaveChangesAsync();

            // creating user's wallet
            var wallet = new Wallet() { Balance = 0, UserId = newUser.Id };

            await unitOfWork.Wallets.CreateAsync(wallet);
            await unitOfWork.SaveChangesAsync();

            return newUser;
        }

        public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            // check for exist
            var user = await unitOfWork.Users.GetAsync(expression);
            if (user is null)
                throw new AppException(404, "User not found!");

            user.State = ItemState.Deleted;
            user.UpdatedAt = DateTime.UtcNow;

            unitOfWork.Users.Update(user);

            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<User>> GetAllAsync(PaginationParams? @params, Expression<Func<User, bool>>? expression = null)
        {
            var users = unitOfWork.Users.Where(expression, isTracking: false);

            return users.Where(user => user.State != ItemState.Deleted).ToPagedList(@params);
        }

        public async Task<UserViewDto> GetAsync(Expression<Func<User, bool>> expression)
        {
            // check for exist
            var user = await unitOfWork.Users.GetAsync(expression);
            if (user is null)
                throw new AppException(404, "User not found");

            var viewModel = mapper.Map<UserViewDto>(user);
            viewModel.Wallets = unitOfWork.Wallets.Where(wallet => wallet.UserId == user.Id, isTracking: false);

            return viewModel;
        }

        public async Task<User> UpdateAsync(long id, UserForCreationDto dto)
        {
            // check for exist
            var user = await unitOfWork.Users.GetAsync(u => u.Id == id && u.State != ItemState.Deleted);
            if (user is null)
                throw new AppException(404, "User not found");

            // check for already exist
            var existLogin = await unitOfWork.Users.AnyAsync(u =>
                u.Login.Equals(dto.Login) && u.State != ItemState.Deleted && u.Id != id);
            if (existLogin)
                throw new AppException(404, "This login already exist");

            user = mapper.Map(dto, user);

            user.State = ItemState.Updated;
            user.UpdatedAt = DateTime.UtcNow;

            user = unitOfWork.Users.Update(user);
            await unitOfWork.SaveChangesAsync();
                
            return user;
        }
    }
}
