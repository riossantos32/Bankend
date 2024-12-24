using System.ComponentModel.DataAnnotations;

namespace Distribuidora.Modelo
{
    public class Proveedores
    {
        public int ID_Proveedores { get; set; }

        [Required]
        [StringLength(50)]
        public required string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public required string Telefono { get; set; }

        [Required]
        [StringLength(100)]
        public required string Correo { get; set; }

        [Required]
        [StringLength(150)]
        public required string Direccion { get; set; }
    }
}
