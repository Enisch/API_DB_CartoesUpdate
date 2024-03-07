using AutoMapper;
using InfraData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraData.Domain.Dto_s____Mappings
{
    public class AutoMapperDtoUser:Profile
    {

        public AutoMapperDtoUser()
        {
            CreateMap<Usuario,DtoUser>().ReverseMap();
        }
    }
}
