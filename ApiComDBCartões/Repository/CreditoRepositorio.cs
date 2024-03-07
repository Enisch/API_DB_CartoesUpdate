using ApiComDBCartões.classes;
using ApiComDBCartões.Interfaces;
using ApiComDBCartões.Models;

namespace ApiComDBCartões.Repository
{
    public class CreditoRepositorio : ICredito
    {

        private Context context;
        

        public CreditoRepositorio(Context context)
        {
            this.context = context;
            

        }

        public void LimiteUsuarioNovo(CartaoCredito credito)
        {
            context.Add(credito);
        }

        void ICredito.DataDaFatura(CartaoCredito credito)
        {
           context.Entry(credito.FaturaOriginal).State= Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        void ICredito.PagarUsandoCartaoCredito(CartaoCredito credito, double Valorcompra)
        {

            if (Valorcompra <= credito.LimiteCartaoCredito)
            {
             context.Entry(credito.LimiteCartaoCredito - Valorcompra).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                
            }
                

            else
                Console.WriteLine("Valor supera o limite atual.");

            //Teste
        }

        async Task<bool> ICredito.TransaçãoRealizadaCredito()
        {
            return await context.SaveChangesAsync()>0; 
        }
    }
}
