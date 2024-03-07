using ContextDb.ContextDB;
using InfraData.Domain;
using InfraData.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories;

namespace ApiComDBCartõesDotNet8.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/(controller)")]
    public class ContaController : Controller
    {
        //private readonly Usuario User;
        private readonly ICreditCard creditCard;
        private readonly IConta conta;
        
        private readonly Context_Db _Db;

        public ContaController(ICreditCard creditCard, IConta conta, Context_Db _Db)
        {
            this.creditCard = creditCard;
            this.conta = conta;
            
            
            this._Db = _Db;

        }


            [HttpGet("Verificar saldo")]//Criar tabela onde o usuario logado fica salvo.
                public async Task<ActionResult> VerifyBalance(Usuario usuario)
                {
                     var Balance = await conta.VerifyBalance(usuario);

                        if( User!= null)
                         return Ok(Balance);

                     return BadRequest("Something is wrong.\nTry later.");
                }

       /* [NonAction] private Usuario TakeUserID() 
        {
           var usuario =  _Db.User.Where(x => x.Id == usuarioController.Userid).FirstOrDefault();
            return usuario!;
        }*/

        [HttpGet("Sem uso")]
        public ActionResult<string> Index()
        {
            return "String de devolução";
        }
    }
}
