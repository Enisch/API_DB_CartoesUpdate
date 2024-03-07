using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraData.Domain.Interfaces
{
    public interface IUsuario
    {
        //only these methods for now.
        public Task<Usuario> RegistrationForUSer(Usuario usuario);
        public Task<Usuario> GetUserByID(int id);

        public Task NewAccount(Usuario usuario);

    }
}
