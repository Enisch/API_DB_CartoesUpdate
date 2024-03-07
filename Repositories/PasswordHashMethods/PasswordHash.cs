using InfraData.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.PasswordHashMethods
{
    public class PasswordHash: IPasswordUserHash
    {

        const int KeySize = 64;
        const int iterations = 10000;
        HashAlgorithmName HashAl = HashAlgorithmName.SHA512;
        char Delimiter = ';';


        public  string HashedPassword(string Password)
        {
            
           var Salt = RandomNumberGenerator.GetBytes(128/8);

            var HashPassword = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(Password), Salt, iterations, HashAl,KeySize);

            return String.Join(Delimiter, Convert.ToBase64String(HashPassword), Convert.ToBase64String(Salt));//Formato original é byte[]
        }

        //Verify user password
        public bool VerifyHashPassword(string PasswordInput, string PasswordHashed)
        {
            var Elements = PasswordHashed.Split(Delimiter);
            var Salt = Convert.FromBase64String(Elements[1]);
            var Hash = Convert.FromBase64String(Elements[0]);

            var HashPasswordToCompare = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(PasswordInput), Salt, iterations, HashAl, KeySize);

            return CryptographicOperations.FixedTimeEquals(Hash, HashPasswordToCompare);

            
        }


    }
}
