using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoBackPeluqueria.Models
{
    [Table("Compras")]
    public class Compra
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("ClienteId")]
        public int ClienteId { get; set; }

        [Required]
        [Column("Fecha")]
        public DateTime Fecha { get; set; }
    }
}
