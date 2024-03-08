using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InfraData.Domain
{
    [Table("usuario")]
    public class Usuario
    {
       /// <Lembrete>
       /// Montar O DTOUsuario e AUToMapper
       /// </summary>
        
            [Key]
            [Column("id")]
            public int Id { get; set; }

             [Column("usuarioNome")]
             [Required(ErrorMessage ="Digite seu NOME completo."),NotNull] 
             [StringLength(50,ErrorMessage ="Seu nome deve ter menos que 81 caracteres.")]
            public string? NomeUsuario { get; set; }


            [Column("usuarioSenha")]
            [Required(ErrorMessage ="Digite uma senha valida."),NotNull]
            public string? SenhaUsuario { get; set; }//Poderia ser em byte[], porém preferi adicionar no formato string como forma de visualização.

             [Column("usuarioEmail")]
             [Required(ErrorMessage ="Digite um Email válido."),NotNull]
             [StringLength(80,ErrorMessage = "Digite no máximo 80 carateres.")]
             public string? EmailUsuario { get; set; }

            [Column("usuarioAgencia")]
            [Required,NotNull]
            [StringLength(7)]
             public string? usuarioAgencia { get; set;}




    }
}
