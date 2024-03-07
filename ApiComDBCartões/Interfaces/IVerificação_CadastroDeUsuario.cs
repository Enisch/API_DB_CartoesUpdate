using ApiComDBCartões.classes;

namespace ApiComDBCartões.Interfaces
{
    public interface IVerificação_CadastroDeUsuario
    {

        public Task<Usuario> GetUsuarios(int idUsuario,string senhaUsuario);
        //Não precisaria usar uma interface,Poderia ser uma classe, porém como forma de treino utilizarei a implementação.

        public void CadastroDeUsuario(Usuario usuario);
        Task<bool> SalvarModificações();
    }
}
