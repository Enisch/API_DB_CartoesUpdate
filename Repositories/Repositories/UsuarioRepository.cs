using ContextDb.ContextDB;
using InfraData.Domain;
using InfraData.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Repositories.PasswordHashMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Repositories.Repositories
{
    //UsuarioRepository implement multiple interfaces to make me learning;
    public class UsuarioRepository : IUsuario,IConta,ICreditCard
    {
        private readonly Context_Db db;
        
        public UsuarioRepository(Context_Db db)
        {
            this.db = db;
           
        }


        //User Methods
        public async Task<Usuario> RegistrationForUSer(Usuario usuario)
        {
 
                 await db.User.AddAsync(usuario);
                 await db.SaveChangesAsync();//Tem de salvar antes de adicionar o metodo NewAccount
                 await NewAccount(usuario);
            
                
            
               
            return usuario!;
        }

        
        public async Task<Usuario> GetUserByID(int id)
        {
            var USerByID = await db.User.Where(x => x.Id == id).FirstOrDefaultAsync();

                return USerByID!;
        }
         
        
        public async Task NewAccount(Usuario usuario)//Insert values into Conta and cartaocredito when User is registered;
        {
            CartaoCredito CC = new CartaoCredito
            {
                LimiteCartaoCredito=1000,//initial value, but it's dependet of the 'Saldo', if the 'saldo' is 0, limite is '1000'
                IdUsuarioCredito= usuario.Id
                
            };

            var Cd = new Conta_debito
            {
                Saldo = 0,
                IDUsuarioDebio = usuario.Id
                
            };

            await db.cartaoCreditos.AddAsync(CC);
            await db.Conta.AddAsync(Cd);
            await db.SaveChangesAsync();

        }
        //Debt Methods,Only these for now
        public async Task<bool> PaymentFromDebit(double Value, Usuario usuario)
        {
            var UserAccount= await db.Conta.Where(x=> x.IDUsuarioDebio == usuario.Id).FirstOrDefaultAsync();

            if(UserAccount!.Saldo>= Value)
            {
                UserAccount.Saldo -= Value;
                db.Conta.Entry(UserAccount).State = EntityState.Modified;
                return true;
            }

            return false;
        }

        public async Task<double> VerifyBalance(Usuario usuario)
        {
            var SaldoConta= await db.Conta.Where(x=> x.IDUsuarioDebio == usuario.Id).FirstOrDefaultAsync();

                return SaldoConta!.Saldo;
        }


        //Credit Card Methods
        public async Task<bool> PaymentUsingCredit(double Value, Usuario usuario)
        {
            var UserCredit = await db.cartaoCreditos.Where(x=> x.IdUsuarioCredito==usuario.Id).FirstOrDefaultAsync();
            
            if(UserCredit!.LimiteCartaoCredito >= Value)
            {
                UserCredit.LimiteCartaoCredito-= Value;
                UserCredit.FaturaOriginal = DateOnly.FromDateTime(DateTime.Now);
                UserCredit.DateInvoiceClosing = DateOnly.FromDateTime(DateTime.Now.AddMonths(1));

                db.cartaoCreditos.Entry(UserCredit).State = EntityState.Modified;
                return true;
            }

            return false;
        }

        public async Task<DateOnly> ConsultInvoice(Usuario usuario)
        {
            var InvoiceDate = await db.cartaoCreditos.Where(x=> x.IDCartaoCredito == usuario.Id).FirstOrDefaultAsync();

            return InvoiceDate!.DateInvoiceClosing;

        }
    }
}
