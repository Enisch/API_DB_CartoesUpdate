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
using System.Threading.RateLimiting;
using System.Threading.Tasks;
using System.Xml;

namespace Repositories.Repositories
{
    //UsuarioRepository implement multiple interfaces to studies purposes;
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
                IdUsuarioCredito= usuario.Id,
                LimiteDisponível = 1000

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
        public async Task<bool> PaymentFromDebit(double Value, int id)
        {
            var UserAccount= await db.Conta.Where(x=> x.IDUsuarioDebio == id).FirstOrDefaultAsync();

            if(UserAccount!.Saldo>= Value)
            {
                UserAccount.Saldo -= Value;
                db.Conta.Entry(UserAccount).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<double> VerifyBalance(int id)
        {
            var SaldoConta= await db.Conta.Where(x=> x.IDUsuarioDebio == id).FirstOrDefaultAsync();
            double Saldo = SaldoConta!.Saldo;
                return Saldo;
        }

        public async Task RaiseBalance(int id,double value)
        {
           var UserToRaiseBalance = await db.Conta.Where(x=> x.IDUsuarioDebio==id).FirstOrDefaultAsync();
            UserToRaiseBalance!.Saldo += value;

            db.Conta.Entry(UserToRaiseBalance).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return;

        }

        //Credit Card Methods
        public async Task<bool> PaymentUsingCredit(double Value, int Id)
        {
            var UserCredit = await db.cartaoCreditos.Where(x=> x.IdUsuarioCredito==Id).FirstOrDefaultAsync();
            
            if(UserCredit!.LimiteCartaoCredito >= Value)
            {
                UserCredit.LimiteDisponível-= Value;
                UserCredit.FaturaOriginal = DateOnly.FromDateTime(DateTime.Now);
                UserCredit.DateInvoiceClosing = DateOnly.FromDateTime(DateTime.Now.AddMonths(1));

                db.cartaoCreditos.Entry(UserCredit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                
                return true;
            }

            return false;
        }

        public async Task<DateOnly> ConsultInvoice(int Id)
        {
            var InvoiceDate = await db.cartaoCreditos.Where(x=> x.IdUsuarioCredito == Id).FirstOrDefaultAsync();

            return InvoiceDate!.DateInvoiceClosing;

        }

        public async Task<double> RaiseCreditLimit(int id)
        {
            
            var contaFromUserId = await db.Conta.Where(x=> x.IDUsuarioDebio ==id).FirstOrDefaultAsync();
            var CreditFromUserID= await db.cartaoCreditos.Where(x=> x.IdUsuarioCredito == id).FirstOrDefaultAsync(); 

            if(CreditFromUserID!.FaturaOriginal == DateOnly.MinValue)
            {
                var NewLimit = contaFromUserId!.Saldo * 0.096;

                var round = Math.Round(NewLimit, 2);
                CreditFromUserID!.LimiteCartaoCredito += round;
                CreditFromUserID!.LimiteDisponível = CreditFromUserID.LimiteCartaoCredito;

                db.Entry(CreditFromUserID).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return CreditFromUserID.LimiteCartaoCredito;
            }

            return 0;
           
        }

            public async Task<double> GetCreditLimit(int id)
             {
                var UserCredit = await db.cartaoCreditos.Where(x=> x.IdUsuarioCredito==id).FirstOrDefaultAsync();

                return UserCredit!.LimiteCartaoCredito;
             }


    }
}
