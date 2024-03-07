using AutoMapper;
using InfraData.Domain;
using InfraData.Domain.Dto_s____Mappings;
using InfraData.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class DtoUserS_Repositorycs : IDtoUser
    {
        private readonly IUsuario _userRepository;
        private readonly IMapper mapperDtoUser;
        private readonly IPasswordUserHash passwordUser; 
        public DtoUserS_Repositorycs(IUsuario userRepository, IMapper mapperDtoUser, IPasswordUserHash passwordUser)
        {

            _userRepository = userRepository;
            this .mapperDtoUser = mapperDtoUser;
            this.passwordUser = passwordUser;

        }

        public string CreateRandomAgencyNumber()
        {
            var randomAgency = new[]
            {
                  "0001-31",
                  "0011-01",
                  "0111-04",
                  "0136-17",
                  "0136-18",
                  "1234-56",
                  "3456-67"
            };

            var randomNumber = new Random();
            var randomtwo=  randomNumber.Next(0,randomAgency.Length-1);
            var returnString = randomAgency[randomtwo];

            return returnString;
        }

        public async Task<DtoUser> FindByIdUserDto(int ID)
        {
          var user = await _userRepository.GetUserByID(ID);
            return mapperDtoUser.Map<DtoUser>(user);

        }

        public async Task<DtoUser> SignUpUserDto(DtoUser user)
        {
           var NewUser = mapperDtoUser.Map<Usuario>(user);

            if(NewUser != null && NewUser.SenhaUsuario != null)
            {
                NewUser.usuarioAgencia = CreateRandomAgencyNumber();
                var Password = passwordUser.HashedPassword(NewUser.SenhaUsuario);
                NewUser.SenhaUsuario = Password;
            }

           var UserRegistred = await _userRepository.RegistrationForUSer(NewUser!);
            return mapperDtoUser.Map < DtoUser>(UserRegistred);
        }
    }
}
