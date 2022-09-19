using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskOfAlifTech.Domain.Enums;
using TaskOfAlifTech.Service.Exceptions;
using TaskOfAlifTech.Service.Extensions;
using TaskOfAlifTech.Service.Interfaces;
using TasOfAlifTech.Data.IRepositories;

namespace TaskOfAlifTech.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }

        public async Task<string> GenerateTokenAsync(string login, string password)
        {
            var user = await unitOfWork.Users.GetAsync(x =>
                x.Login == login && x.Password == StringExtension.PasswordHash(password) && x.State != ItemState.Deleted);

            if (user is null)
                throw new AppException(400, "Login or password is incorrect");


            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("X-UserId", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
