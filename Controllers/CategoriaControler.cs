using Microsoft.AspNetCore.Mvc;
using Distribuidora.Modelo;
using Distribuidora.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Distribuidora.Controllers
{
    //Esta linea define la ruta base para el controlador
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaControler : ControllerBase
    {
        private readonly CategoriaServices _categoriaServices;

        public CategoriaControler (CategoriaServices estudianteServices)
        {
            _categoriaServices = estudianteServices;
        }

        //Llama al metodo que esta en la clase categoriaServices para Mostrar los datos,
        //lo cual respondera la solicitud HTTPS
        [HttpGet]
        [Route("ObtenerId/{id}")]
        public async Task<ActionResult<List<Categoria>>> GetId(int id)
        {
            var categoria = await _categoriaServices.GetCategoria( id);
            return Ok(categoria);
        }

        //Llama al metodo que esta en la clase categoriaServices para insertar  datos/peticion httpPost
        [HttpPost]
    [Route("api/Insertar")]
    public async Task<ActionResult> Post(Categoria Oestudiante)
    {

        await _categoriaServices.InsertarCategoria(Oestudiante);
        return Ok("Categoria Insertada");
    }
        //Llama al metodo que esta en la clase categoriaServices para Actualizar los datos
        [HttpPut]
    [Route("Actualizar")]
    public async Task<ActionResult> Put(Categoria Oestudiante)
    {
        await _categoriaServices.ActualizarCategoria(Oestudiante);
        return Ok("Categoria Actualizada");
    }
        //Llama al metodo que esta en la clase categoriaServices para Eliminar los datos
        [HttpDelete]
    [Route("Delete")]
    public async Task<ActionResult> Delete(int id)
    {
        var resultado = await _categoriaServices.DeleteCategoria(id);
        return Ok("Categoria Eliminado");
    }
}
}
    



