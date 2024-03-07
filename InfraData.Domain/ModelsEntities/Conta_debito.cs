using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InfraData.Domain
{
    [Table("conta_debito")]
    public class Conta_debito
    {
        [Key]
        [Column("idCartao")]
        public int IDCartaoDebito { get; set;}

        [Column("Saldo")]
        [Required,NotNull]
        public double Saldo { get; set;}

        [ForeignKey("Fk_usuario")]
        [Column("idUsuarioDebito")]
        [Required, NotNull]
        public int IDUsuarioDebio { get; set;}
    }
}
