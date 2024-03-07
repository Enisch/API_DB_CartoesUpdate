using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraData.Domain.Dto_s____Mappings
{
    public class DtoUser
    {/// <Lembrete do que é DTo>
     /// Objeto de transferencia de dados = data object transfer DTo
     /// </summary>

        [NotMapped]
        public int Id { get; set; }


        [Required(ErrorMessage = "Digite seu NOME completo."), NotNull]
        [StringLength(50, ErrorMessage = "Seu nome deve ter menos que 81 caracteres.")]
        public string? NomeUsuario { get; set; }

        
        [Required(ErrorMessage = "Digite uma senha valida."), NotNull]
        [StringLength(20)]
        [NotMapped]
        public string? SenhaUsuario { get; set; }

        [Required(ErrorMessage = "Digite um Email válido."), NotNull]
        [StringLength(80, ErrorMessage = "Digite no máximo 80 carateres.")]
        public string? EmailUsuario { get; set; }



    }
}
