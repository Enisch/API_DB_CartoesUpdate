using ApiComDBCartões.classes;

namespace ApiComDBCartões.Interfaces
{
    public interface ICredito
    {

        public void PagarUsandoCartaoCredito(CartaoCredito credito, double ValorCompra);

        public void DataDaFatura(CartaoCredito credito);

        public void LimiteUsuarioNovo(CartaoCredito credito);

        Task<bool> TransaçãoRealizadaCredito();



    }
}
