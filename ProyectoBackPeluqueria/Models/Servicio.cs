using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoBackPeluqueria.Models
{
    [Table("Servicios")]
    public class Servicio
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Column("Descripcion")]
        public string Descripcion { get; set; }

        [Required]
        [Column("DuracionMinutos")]
        public int DuracionMinutos { get; set; }

        [Required]
        [Column("Precio")]
        public decimal Precio { get; set; }
    }
}
