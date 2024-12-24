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
    public class ClienteControler : ControllerBase
    {
        private readonly ClienteServices _clienteServices;

        public ClienteControler(ClienteServices clienteServices)
        {
            _clienteServices = clienteServices;
        }

        //Llama al metodo que esta en la clase categoriaServices para Mostrar los datos
        //lo cual respondera la solicitud HTTPS
        [HttpGet]
        [Route("ObtenerId/{id}")]
        public async Task<ActionResult<List<Cliente>>> GetId(int id)
        {
            var cliente = await _clienteServices.GetCliente(id);
            return Ok(cliente);
        }

        //Llama al metodo que esta en la clase ClienteServices para insertar  datos/peticion httpPost
        [HttpPost]
        [Route("Insertar")]
        public async Task<ActionResult> Post(Cliente Ocliente)
        {

            await _clienteServices.InsertarCliente(Ocliente);
            return Ok("Cliente Insertada");
        }
    
        //Llama al metodo que esta en la clase categoriaServices para Actualizar los datos
        [HttpPut]
        [Route("Actualizar")]
        public async Task<ActionResult> Put(Cliente Oclientes)
        {
            await _clienteServices.ActualizarCliente(Oclientes);
            return Ok("Cliente Actualizado");
        }

        //Llama al metodo que esta en la clase clienteServices para Eliminar los datos
        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var resultado = await _clienteServices.DeleteCliente(id);
            return Ok("Cliente Eliminado");
        }
    }
}
    
