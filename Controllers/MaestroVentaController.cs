using Distribuidora.Modelo;
using Distribuidora.Services;
using Microsoft.AspNetCore.Mvc;

namespace Distribuidora.Controllers
{
   
        [Route("api/[controller]")]
        [ApiController]
        public class MaestroVentaController : ControllerBase
        {
            private readonly MaestroVentaServices _ventaServices;

            public MaestroVentaController(MaestroVentaServices ventaServices)
            {
            _ventaServices = ventaServices;
            }


            [HttpPost]
        [Route("api/InsertarVenta")]
        public async Task<ActionResult> Post(VentaFactura Oventa)
        {

            await _ventaServices.InsertarVentaFactura(Oventa);
            return Ok("DetalleVenta Insertado");

            //
        }
        [HttpPost]
        [Route("api/InsertarDetalle")]
        public async Task<ActionResult> PostDetalle(MaestroDetalle_venta Odetalle)
        {
          

            await _ventaServices.InsertarDetalleVentaFactura(Odetalle);
            return Ok("DetalleVenta Insertado");
        }


    }
}

