using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoBackPeluqueria.Models
{
    [Table("CompraDetalles")]
    public class CompraDetalle
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("CompraId")]
        public int CompraId { get; set; }

        [Required]
        [Column("ProductoId")]
        public int ProductoId { get; set; }

        [Required]
        [Column("Cantidad")]
        public int Cantidad { get; set; }

        [Required]
        [Column("PrecioUnitario")]
        public decimal PrecioUnitario { get; set; }
    }
}
