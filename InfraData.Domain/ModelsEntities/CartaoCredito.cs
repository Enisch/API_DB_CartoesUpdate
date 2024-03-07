using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InfraData.Domain
{
    [Table("cartaocredito")]
    public class CartaoCredito
    {
        [Key]
        [Column("idCartaoCredito")]
        public int IDCartaoCredito { get; set;}

        [Column("Limite")]
        [Required,NotNull]
        public double LimiteCartaoCredito { get; set;}

        [Column("FaturaData")]
        public DateOnly FaturaOriginal {  get; set;}


        [Column("FaturaProximaData")]
        public DateOnly FaturaProximaData { get; set;}

        [Column("DateInvoiceClosing")]
        public DateOnly DateInvoiceClosing { get; set;}

        [ForeignKey("Fk_usuarioCredito")]
        [Column("idUsuarioCredito")]
        [Required,NotNull]
        public int IdUsuarioCredito { get; set;}


    }
}
