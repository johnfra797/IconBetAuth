using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IconBetAuth.Domain.DTO;
using IconBetAuth.Domain.Models;

namespace IconBetAuth.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Hall, HallDTO>();
            CreateMap<TransactionDTO, Transaction>();
        }
    }
    
}
