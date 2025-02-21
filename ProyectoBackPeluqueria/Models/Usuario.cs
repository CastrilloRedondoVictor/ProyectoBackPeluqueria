using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoBackPeluqueria.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Column("Apellidos")]
        public string Apellidos { get; set; }

        [Required]
        [Column("Telefono")]
        public string Telefono { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("ColorPelo")]
        public string ColorPelo { get; set; }

        [Column("Imagen")]
        public string Imagen { get; set; }

        [Required]
        [Column("IdRolUsuario")]
        public int IdRolUsuario { get; set; }
    }
}
