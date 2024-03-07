using ContextDb.ContextDB;
using InfraData.Domain;
using InfraData.Domain.Dto_s____Mappings;
using InfraData.Domain.Interfaces;
using InfraData.Domain.ModelsEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.JWT_TokenGenerator;
using Repositories.LoginUser_Authorize;
using Repositories.PasswordHashMethods;
using Repositories.Repositories;

namespace ApiComDBCartõesDotNet8.Controllers
{
    [ApiController]
    [Route("api/(controller)")]
    public class UsuarioController : Controller//DONE
    {
       
        private readonly IDtoUser dtoUserS_;
        private readonly LoginAuthorize login;
        private readonly TokenGenerator_JWT jWT;
        

        public UsuarioController(IDtoUser dtoUserS_,LoginAuthorize login,TokenGenerator_JWT jWT)
        {
           this.login = login;
           this.dtoUserS_ = dtoUserS_;
           this. jWT = jWT;
        }

        [Authorize]
        [HttpGet("Find User By Id{id}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetByID(int id) 
        {
            var UserID = await dtoUserS_.FindByIdUserDto(id);

            if (UserID != null)
            {
               
                return Ok(UserID);
            }
              

            return NotFound("User not Founded.");
        }
        
        //Registration for User;
        [HttpPost("Sign-up")]
        public async Task<ActionResult> CadastroCliente(DtoUser usuario)
        {

            //If true, it's means User Exists, therefore, can't sign-up again.
            if (await login.CheckAvailbilityEmailUser(usuario.EmailUsuario))
                return BadRequest("Email indisponível.\nTente outro.");

                var NewUSer = await dtoUserS_.SignUpUserDto(usuario);

            if (NewUSer != null) 
            {  
                return Ok("User registered sucessfuly.");
            }


            return BadRequest("Something is wrong.\nTry again.");
        }

        //Login User;
        [HttpPost("Sign-In")]
        public async Task<ActionResult<Jwt_Token>> LoginCliente(LoginModels loginUser)
        {
            //Verify Email. If it's True OK;
            var checkingEmail= await login.CheckAvailbilityEmailUser(loginUser.Email!);

            if (!checkingEmail)
                return Unauthorized("O email informado não corresponde a nenhum usuario.");


            //Verify Agency number. if it's OK;
            var CheckingAgency = await login.CheckIFUserAgencyIsCorrect(loginUser.AgencyNumber!);

            if (!CheckingAgency)
                return Unauthorized("A agencia está incorreta.");

            //Get the User through Email.
            var User = await login.GetUserByEmail(loginUser.Email!);


            //Verify Password. If it's true OK;
            var CheckingPassword = login.CheckIfUserPasswordISReal(loginUser.Password!, User);

            if (!CheckingPassword)
                return Unauthorized("Usuario ou senha incorretos.");

            //All check's Done. Return Token to Authenticaticate USER;
            var token = jWT.Generator_JWT(User);

            return new Jwt_Token
            {
                Token = token
            };
        }

    }
}
