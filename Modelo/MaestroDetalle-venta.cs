using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Distribuidora.Modelo
{
    public class VentaFactura 
    {
        public int NumeroFactura { get; set; }
        public int idCliente { get; set; }
        public DateTime Fecha { get; set; }
    }
}
public class MaestroDetalle_venta
    {
        public int ID_Detalle { get; set; }
        public int IDProducto { get; set; }
        public int NumeroFactura { get; set; }
        public int Cantidad { get; set; }
        public Double precioVenta { get; set; }
        public int Stock { get; set; }
        public Double total { get; set; }
        public Double subTotal { get; set; }
    }

