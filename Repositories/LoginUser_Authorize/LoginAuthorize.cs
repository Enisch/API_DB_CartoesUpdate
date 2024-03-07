using AutoMapper;
using ContextDb.ContextDB;
using InfraData.Domain;
using InfraData.Domain.Dto_s____Mappings;
using InfraData.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.LoginUser_Authorize
{
    //Decide não utilizar Interface nesse metodo.
    public class LoginAuthorize
    {
        private readonly Context_Db _Db;
        private readonly IPasswordUserHash passwordUserHash;
        public LoginAuthorize(Context_Db _Db,IPasswordUserHash passwordUserHash)
        {
            this._Db = _Db;
            this.passwordUserHash = passwordUserHash;
        }

        //It's checking if the user exists. If the user exists, return true, otherwise return false;
        public async Task<bool> CheckAvailbilityEmailUser(string email)
        {
            var User = await _Db.User.Where(x=> x.EmailUsuario == email).FirstOrDefaultAsync();

            if (User != null)
                return true;

            return false;

        }

        public async Task<bool> CheckIFUserAgencyIsCorrect(string agency)
        {
            var User = await _Db.User.Where(x=> x.usuarioAgencia == agency).FirstOrDefaultAsync();

            if (User != null)
                return true;
            
            return false;
        }

        public async Task<Usuario> GetUserByEmail(string email)
        {
            var UserVerifying= await _Db.User.Where(x=> x.EmailUsuario == email).FirstOrDefaultAsync();
            
            return UserVerifying!;  
        }

        public bool CheckIfUserPasswordISReal(string senha,Usuario usuario)
        {
            var CheckPassword = passwordUserHash.VerifyHashPassword(senha, usuario.SenhaUsuario);
            if (!CheckPassword)
                return false;

            return true;
        }


       

    }
}
