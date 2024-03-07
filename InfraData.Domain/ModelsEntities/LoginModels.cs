using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraData.Domain.ModelsEntities
{
    public class LoginModels
    {
        [Required(ErrorMessage ="Insira um E-mail válido."),EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage ="Insira uma senha válida."),PasswordPropertyText]
        public string? Password { get; set; }
        [Required(ErrorMessage =" Insira o número da agencia adequada.")]
        public string? AgencyNumber { get; set; }


    }
}
