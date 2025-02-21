using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoBackPeluqueria.Models
{
    [Table("Productos")]
    public class Producto
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Column("Stock")]
        public int Stock { get; set; }

        [Required]
        [Column("Precio")]
        public decimal Precio { get; set; }

        [Column("Imagen")]
        public string Imagen { get; set; }
    }
}
