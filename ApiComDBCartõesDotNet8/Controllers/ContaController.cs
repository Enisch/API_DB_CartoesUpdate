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
        

        public ContaController(ICreditCard creditCard, IConta conta, Context_Db _Db)
        {
            this.creditCard = creditCard;
            this.conta = conta;
 
        }

                /// <Conta>
                /// All method's from Conta 
                /// </summary>
                /// <returns></returns>

            [HttpGet("Verificar saldo")]
                public async Task<ActionResult<string>> VerifyBalance()
                {
                         
                     var Balance = await conta.VerifyBalance(TakeUserID());

                        if( User!= null)
                        {
                         var TextoSaldo = ($"Saldo atual: R${Balance}");
                            return Ok(TextoSaldo);
                        }
    
                     return BadRequest("Something is wrong.\nTry later.");
                }
        //Get User id from CLAIMS.
        [NonAction] private int TakeUserID() 
        {
            var usuarioID = Convert.ToInt32(User.FindFirst("id")!.Value);
            return usuarioID;
        }

        //Method to modify the balance and, in the future, make payments between 2 accounts.
        [HttpPatch("Realizar pagamento com Saldo da conta.{value}")]
        public async Task<ActionResult<string>> PaymentFromDebt(double value)
        {
           var SucessOrNot = await conta.PaymentFromDebit(value, TakeUserID());

            if (!SucessOrNot)
                return BadRequest("Pagamento não pode ser concluido.\nSaldo ou informações insuficientes.");

            var Balance = await conta.VerifyBalance(TakeUserID());
            var atualBalance = $"Seu novo saldo é de: R${Balance}";
            return Ok($"Pagamento Realizado.\n{atualBalance}.");
        }

        //The goal of this method is self explanatory.It's for test purposes.
        [HttpPatch("Method To Raise Initial Balance,{value}")] 
        public async Task<ActionResult<string>> RaiseInitialBalance(double value)
        {
            await conta.RaiseBalance(TakeUserID(), value);
            var Balance = await conta.VerifyBalance(TakeUserID());
            var atualBalance = ($"Seu novo saldo é de: R${Balance}");
            return Ok(atualBalance);
        }




        //Credit method's

        [HttpGet("Consult Invoices")]
        public async Task<ActionResult<string>> ConsultingForInvoices()
        {
           var Invoice = await creditCard.ConsultInvoice(TakeUserID());

            if (Invoice == DateOnly.MinValue)
                return Ok("Nenhuma pendencia.");

            var InvoiceInitial = Invoice.AddMonths(-1);
            return Ok($"Fatura existente.\n Data do fechamento da fatura:{Invoice}.\nFatura aberta desde:{InvoiceInitial}.");

        }
        [HttpPatch("Payment using Credit Card")]
        public async Task<ActionResult> PaymentWithCredit(double value)
        {
            var SucessOrNot = await creditCard.PaymentUsingCredit(value, TakeUserID());

            if (!SucessOrNot)
                return BadRequest("O valor disponível no cartão é inferior ao valor da compra.");

            return Ok("Pagamento efetuado.");
        }

        //It's the same purpose from the method RaiseInitialBalance.
        [HttpPatch("Raise Credit Limit")]
        public async Task<ActionResult<string>> RaiseCredit()
        {
            var PreviousLimit = await creditCard.GetCreditLimit(TakeUserID());
            var NewLimit=await creditCard.RaiseCreditLimit(TakeUserID());

            if (NewLimit > PreviousLimit)
                return Ok($"Seu novo limite foi aprovado.\nNovo limite:R${NewLimit}");

            return BadRequest("O aumento de limite não pode ser aprovado.\nTente novamente mais tarde.");

        }

    }
}
