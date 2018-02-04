using AutoMapper;
using Core.Domain.Finance;
using Core.Domain.PersonAndData;
using Smart.Models.PersonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Person, PersonModel>();
            CreateMap<Address, PersonModel>();

            CreateMap<PersonModel, Address>();
            CreateMap<PersonModel, Person>();

           




        }
    }
}