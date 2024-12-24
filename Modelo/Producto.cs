using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace Distribuidora.Modelo
{
    public class Producto
    {
        
        public int ID_Producto { get; set; }

        [Required]
        [StringLength(40)] 
        public required String NombreProducto { get; set; }

        [Required]
        [StringLength(60)]
        public required  String Descripcion { get; set; }

        public int Stock { get; set; }

        public int ID_Categoria { get; set; }

        public float PrecioCompra { get; set; }

        public float PrecioVenta { get; set; }

        public string? UbicacionFotografia { get; set; } /* esto significa que es opcional*/
        public IFormFile Fotografia { get; set; }

    }

}
