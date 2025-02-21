using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoBackPeluqueria.Models
{
    [Table("HorariosDisponibles")]
    public class HorarioDisponible
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Fecha")]
        public DateTime Fecha { get; set; }

        [Required]
        [Column("HoraInicio")]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        [Column("Disponible")]
        public bool Disponible { get; set; }
    }
}
