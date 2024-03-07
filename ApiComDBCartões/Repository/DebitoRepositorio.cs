using ApiComDBCartões.classes;
using ApiComDBCartões.Interfaces;
using ApiComDBCartões.Models;
using MySqlConnector;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ApiComDBCartões.Repository
{
    public class DebitoRepositorio : IDebito
    {
        private Context context;

        public DebitoRepositorio(Context context)
        {
           this.context = context;
            
        }

        public void SaldoUsuarioNovoAsync(Conta_debito _Debito)
        {
            _Debito.Saldo = 0;
            
           context.Add(_Debito);
            

           
        }

        void IDebito.PagarUsandoDebito(Conta_debito _Debito,double ValorCompra)
        {
            if (ValorCompra <= _Debito.Saldo)
                context.Entry(_Debito.Saldo - ValorCompra).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            else
                Console.WriteLine("Saldo menor que o valor da compra.");
        }

        async Task<bool> IDebito.TransacaoEfetuada()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
