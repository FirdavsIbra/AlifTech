using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOfAlifTech.Domain.Entities.Users;
using TaskOfAlifTech.Domain.Entities.Transactions;
using TaskOfAlifTech.Service.DTOs.Users;
using TaskOfAlifTech.Service.DTOs.Transactions;

namespace TaskOfAlifTech.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForCreationDto, User>();
            CreateMap<User, UserViewDto>();

            CreateMap<TransactionForCreationDto, Transaction>();
            CreateMap<TransactionToAddMoneyDto, Transaction>();
            CreateMap<Transaction, TransactionViewDto>();
            CreateMap<IQueryable<Transaction>, List<TransactionViewDto>>();
        }
    }
}
