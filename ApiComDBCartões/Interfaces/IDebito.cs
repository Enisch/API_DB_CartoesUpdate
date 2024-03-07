using ApiComDBCartões.classes;

namespace ApiComDBCartões.Interfaces
{
    public interface IDebito
    {

        public void PagarUsandoDebito(Conta_debito _Debito,double ValorCompra);//Altera o saldo da conta.
        Task<bool> TransacaoEfetuada();//Salva a Modificação;

        void SaldoUsuarioNovoAsync(Conta_debito _Debito);

    }



}
