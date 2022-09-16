using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TaskOfAlifTech.Domain.Configurations;
using TaskOfAlifTech.Domain.Entities.Users;
using TaskOfAlifTech.Domain.Enums;
using TaskOfAlifTech.Service.DTOs.UserForCreation;
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
            var user = await unitOfWork.Users.GetAsync(u => 
                u.Login == dto.Login && u.State != ItemState.Deleted);
            if (user is not null)
                throw new AppException(400, "User already exist!");
            
            // map entity
            var newUser = mapper.Map<User>(dto);

            newUser = await unitOfWork.Users.CreateAsync(newUser);
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
            user.UpdatedAt = DateTime.Now;

            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<User>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null)
        {
            var users = unitOfWork.Users.GetAllAsync(expression, isTracking: false);

            return await users.ToPagedList(@params).ToListAsync();
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> expression)
        {
            // check for exist
            var user = await unitOfWork.Users.GetAsync(expression);
            if (user is null)
                throw new AppException(404, "User not found");

            return user;
        }

        public async Task<User> UpdateAsync(long id, UserForCreationDto dto)
        {
            // check for exist
            var user = await unitOfWork.Users.GetAsync(u => u.Id == id && u.State != ItemState.Deleted);
            if (user is null)
                throw new AppException(404, "User not found");

            // check for already exist
            var existUser = await unitOfWork.Users.GetAsync(u => 
                u.Login.Equals(dto.Login) && u.State != ItemState.Deleted);
            if (existUser is not null)
                throw new AppException(404, "This login already exist");

            var mappedUser = mapper.Map(dto, user);

            mappedUser.State = ItemState.Updated;
            mappedUser.UpdatedAt = DateTime.UtcNow;

            var updatedUser = await unitOfWork.Users.UpdateAsync(mappedUser);

            await unitOfWork.SaveChangesAsync();
                
            return updatedUser;
        }
    }
}
