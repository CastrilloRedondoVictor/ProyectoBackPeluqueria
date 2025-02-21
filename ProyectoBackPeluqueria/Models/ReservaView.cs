using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoBackPeluqueria.Models
{
    [Table("Vista_Reservas")]
    public class ReservaView
    {
        [Key]
        [Column("ReservaID")]
        public int ReservaID { get; set; }
        [Column("Cliente")]
        public string Cliente { get; set; }
        [Column("Servicio")]
        public string Servicio { get; set; }
        [Column("FechaHoraInicio")]
        public DateTime FechaHoraInicio { get; set; }
        [Column("FechaHoraFin")]
        public DateTime FechaHoraFin { get; set; }
    }
}
