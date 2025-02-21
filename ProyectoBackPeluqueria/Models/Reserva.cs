using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoBackPeluqueria.Models
{
    [Table("Reservas")]
    public class Reserva
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("ClienteId")]
        public int ClienteId { get; set; }

        [Required]
        [Column("ServicioId")]
        public int ServicioId { get; set; }

        [Required]
        [Column("FechaHoraInicio")]
        public DateTime FechaHoraInicio { get; set; }

        [Column("FechaHoraFin")]
        public DateTime? FechaHoraFin { get; set; }
    }
}
