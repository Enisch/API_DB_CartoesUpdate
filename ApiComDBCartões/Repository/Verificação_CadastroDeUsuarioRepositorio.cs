using ApiComDBCartões.classes;
using ApiComDBCartões.Interfaces;
using ApiComDBCartões.Models;

namespace ApiComDBCartões.Repository
{
    public class Verificação_CadastroDeUsuarioRepositorio : IVerificação_CadastroDeUsuario
    {

        private Context context; 
        public Verificação_CadastroDeUsuarioRepositorio(Context context)
        {
            this.context = context;
        }
        void IVerificação_CadastroDeUsuario.CadastroDeUsuario(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
           
        }

        Task<Usuario> IVerificação_CadastroDeUsuario.GetUsuarios(int idUsuario, string senhaUsuario)
        {
            throw new NotImplementedException();
        }

        async Task<bool> IVerificação_CadastroDeUsuario.SalvarModificações()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
