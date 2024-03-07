using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraData.Domain.Interfaces
{
    public interface IPasswordUserHash
    {
        public string HashedPassword(string Password);
        public bool VerifyHashPassword(string PasswordInput, string PasswordHashed);
    }
}
