using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoBackPeluqueria.Models
{
    [Table("RolesUsuario")]
    public class RolUsuario
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Nombre")]
        public string Nombre { get; set; }
    }

}
