using System.ComponentModel.DataAnnotations;

namespace Distribuidora.Modelo
{
    public class Categoria
    {
        public int ID_Categoria { get; set; }

        [Required]
        [StringLength(50)]
        public required String nombreCategoria { get; set; }

 
    }

}


