using Microsoft.AspNetCore.Mvc;
using Distribuidora.Modelo;
using Distribuidora.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Distribuidora.Controllers
{
    //[ApiController] indica que esta clase es un controlador de API, lo
    //que habilita ciertas funcionalidades como la validación automática de modelos y la
    //inferencia de rutas.
    [Route("api/[controller]")]
    [ApiController]
    public class proveedorControler : ControllerBase
    {
        private readonly ProveedorServices _proveedorServices;

        public proveedorControler(ProveedorServices proveedorServices)
        {
            _proveedorServices = proveedorServices;
        }

        //Llama al metodo que esta en la clase categoriaServices para Mostrar los datos
        //lo cual respondera la solicitud HTTPS
        [HttpGet]
        [Route("ObtenerId/{id}")]
        public async Task<ActionResult<List<Proveedores>>> GetId(int id)
        {
            var proveedor = await _proveedorServices.GetProveedor(id);
            return Ok(proveedor);
        }

        [HttpPost]
        [Route("Insertar")]
        public async Task<ActionResult> Post(Proveedores Oproveedor)
        {

            await _proveedorServices.InsertarProveedor(Oproveedor);
            return Ok("Proveedor Insertado");
        }
        //Llama al metodo que esta en la clase proveedorServices para Actualizar los datos
        [HttpPut]
        [Route("Actualizar")]
        public async Task<ActionResult> Put(Proveedores Oproveedor)
        {
            await _proveedorServices.ActualizarProveedor(Oproveedor);
            return Ok("Proveedor Actualizado");
        }

        //Llama al metodo que esta en la clase proveedorServices para Eliminar los datos
        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var resultado = await _proveedorServices.DeleteProveedor(id);
            return Ok("proveedor Eliminado");
        }
    }
}
