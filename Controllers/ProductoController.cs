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
    public class ProductoControler : ControllerBase
    {
        private readonly ProductoService _productoServices;

        public ProductoControler(ProductoService productoServices)
        {
            _productoServices = productoServices;
        }
        [HttpGet]
        [Route("buscarProducto/{nombreProducto}")]
        public async Task<ActionResult<List<Producto>>> buscarProducto(String nombreProducto)
        {
            var producto = await _productoServices.buscarProducto(nombreProducto);
            return Ok(producto);
        }

        [HttpGet]
        [Route("ObtenerTodos")]
        public async Task<ActionResult<List<Producto>>> GetId()
        {
            var productost = await _productoServices.GetTodosProductos();

            return Ok(productost);
        }

        //Llama al metodo que esta en la clase productoServices para Mostrar los datos
        //lo cual respondera la solicitud HTTPS
        [HttpGet]
        [Route("ObtenerId/{id}")]
        public async Task<ActionResult<List<Producto>>> GetId(int id)
        {
            var producto = await _productoServices.GetProducto(id);
            return Ok(producto);
        }

        //Llama al metodo que esta en la clase productoServices para insertar  datos/peticion httpPost
        [HttpPost]
        [Route("Insertar")]
        public async Task<ActionResult> Post(Producto Oproductos)
        {

            await _productoServices.InsertarProducto(Oproductos);
            return Ok("Producto Insertado");
        }

        //Llama al metodo que esta en la clase productoServices para Actualizar los datos
        [HttpPut]
        [Route("Actualizar")]
        public async Task<ActionResult> Put(Producto Oproducto)
        {
            await _productoServices.ActualizarProducto(Oproducto);
            return Ok("Producto Actualizado");
        }
        //Llama al metodo que esta en la clase categoriaServices para Eliminar los datos
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var resultado = await _productoServices.DeleteProducto(id);
            return Ok("Producto Eliminado");
        }

        //descargar fotografía
        [HttpPost("bajarfotografia")]
        public ActionResult downloadfile([FromForm] string nombreArchivo)
        {
            try
            {
                var (fileBytes, mimeType, fileName) = _productoServices.ObtenerFotografia(nombreArchivo);
                return File(fileBytes, mimeType, fileName);
            }
            catch (FileNotFoundException ex)
            {
                // Opcional: Registrar el error o imprimirlo en la consola
                Console.WriteLine("Error: " + ex.Message);
                return NotFound("El archivo no fue encontrado.");
            }
            catch (Exception ex)
            {
                // Opcional: Registrar el error o imprimirlo en la consola
                Console.WriteLine("Error general: " + ex.Message);
                return NotFound("Error al intentar descargar el archivo.");
            }
        }
    }
}




 