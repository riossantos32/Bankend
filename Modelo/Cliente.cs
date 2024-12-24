using System.ComponentModel.DataAnnotations;

namespace Distribuidora.Modelo
{
    public class Cliente
    {
       public int ID_Cliente { get; set; }

        [Required]
        [StringLength(50)]
        public required string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public required string Apellido { get; set; }

        [Required]
        [StringLength(50)]
        public required string  Tipo_Cliente { get; set; }

        [Required]
        [StringLength(50)]
        public required string Email { get; set; }

        [Required]
        [StringLength(8)]
        public required string Contraseña { get; set; }
    }
}
