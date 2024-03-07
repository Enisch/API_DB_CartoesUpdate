using InfraData.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.JWT_TokenGenerator
{
    public class TokenGenerator_JWT
    {
        

        private readonly IConfiguration configuration;

        public TokenGenerator_JWT(IConfiguration configuration)
        {

            this.configuration = configuration;

        }
        public string Generator_JWT(Usuario User)
        {
            var key = Encoding.UTF8.GetBytes(configuration["JwtTokenConfig:SecretKey"]!);
            var TokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", User.Id.ToString()),
                    new Claim("Name", User.NomeUsuario),
                    new Claim("Email", User.EmailUsuario),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),

                Expires = DateTime.UtcNow.AddMinutes(7),
                Issuer = configuration["JwtTokenConfig:Issuer"],
                Audience = configuration["JwtTokenConfig:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512)

        };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenCreate = tokenHandler.CreateToken(TokenDescriptor);
            var Token = tokenHandler.WriteToken(tokenCreate);

            return Token;
            
        }



    }
}
