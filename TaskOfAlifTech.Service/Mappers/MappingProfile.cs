using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOfAlifTech.Domain.Entities.Users;
using TaskOfAlifTech.Service.DTOs.UserForCreation;

namespace TaskOfAlifTech.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForCreationDto, User>();
        }
    }
}
