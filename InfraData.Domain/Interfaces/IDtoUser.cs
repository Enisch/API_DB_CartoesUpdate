using InfraData.Domain.Dto_s____Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraData.Domain.Interfaces
{
       public  interface IDtoUser
    {

        public Task<DtoUser> SignUpUserDto(DtoUser user);
        public Task<DtoUser> FindByIdUserDto(int ID);

        public string CreateRandomAgencyNumber();
    }
}
 