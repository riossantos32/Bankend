using Distribuidora.Modelo;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Dapper;


namespace Distribuidora.Services
{
    public class ProductoService
    {
        private readonly IConfiguration _configuration;
        private readonly String _StringSql;
        public  string _RutaArchivo;

        public ProductoService(IConfiguration configuration)
        {
            _configuration = configuration;
            _StringSql = _configuration.GetConnectionString("MyDB")!;
            _RutaArchivo = _configuration.GetSection("servidor").GetSection("Ruta").Value;
        }
        public async Task<Producto> buscarProducto(string nombreProducto)
        {

            string _queryCommand = "buscarProducto";
            var parametro = new DynamicParameters();
            parametro.Add("@parametroBusqueda",nombreProducto, dbType: DbType.String);
            using (var con = new SqlConnection(_StringSql))
            {
                var _producto = await con.QueryFirstOrDefaultAsync<Producto>(
                    _queryCommand, parametro, commandType: CommandType.StoredProcedure);
                return _producto;
            }
        }

        //todos
        //todos
        public async Task<List<Producto>> GetTodosProductos()
            {
                string queryCommand = "listarTodosProducto";

                using (var con = new SqlConnection(_StringSql))
                {
                    var productos = await con.QueryAsync<Producto>(queryCommand, commandType: CommandType.StoredProcedure);
                    return productos.ToList();
                }
            }
        

        //Metodo para listar Producto pasandole como parametro el id
        public async Task<Producto> GetProducto(int id)
        {

            string _queryCommand = "listarProducto";
            var parametro = new DynamicParameters();
            parametro.Add("@IdProducto", id, dbType: DbType.Int32);
            using (var con = new SqlConnection(_StringSql))
            {
                var _producto = await con.QueryFirstOrDefaultAsync<Producto>(
                    _queryCommand, parametro, commandType: CommandType.StoredProcedure);
            
                return _producto;

            }
        }
        //metodo para insertar Producto pasandoles como parametro los atributos
        // al que se le requiera insertar un valor
        public async Task<string> InsertarProducto([FromBody] Producto _Oproductob)
        {
            string extension = Path.GetExtension(_Oproductob.Fotografia.FileName);
            string nuevoNombreFoto = _Oproductob.NombreProducto + extension;
            string _UbicacionFotografia = Path.Combine(_RutaArchivo, nuevoNombreFoto);
            try
            {
                using (FileStream FotografiaProducto = System.IO.File.Create(_UbicacionFotografia))
                {

                    _Oproductob.Fotografia.CopyTo(FotografiaProducto);
                    FotografiaProducto.Flush();
                }   

                string _queryCommand = "insertarProducto";
            var parametro = new DynamicParameters();
                parametro.Add("@NombreProducto ", _Oproductob.NombreProducto, dbType: DbType.String);
                parametro.Add("@Descripcion", _Oproductob.Descripcion, dbType: DbType.String);
                parametro.Add("@Stock", _Oproductob.Stock, dbType: DbType.Int32);
                parametro.Add("@IDCategoria", _Oproductob.ID_Categoria, dbType: DbType.Int32);
                parametro.Add("@PrecioCompra", _Oproductob.PrecioCompra, dbType: DbType.Double);
                parametro.Add("@PrecioVenta", _Oproductob.PrecioVenta, dbType: DbType.Double); 
            parametro.Add("@imagen", nuevoNombreFoto, dbType: DbType.String);

            using (var con = new SqlConnection(_StringSql))
            {

                await con.ExecuteAsync(_queryCommand, parametro,
                    commandType: CommandType.StoredProcedure);
                return ("Producto Insertado");
                }
            }
            catch (Exception ex)
            {
                return (ex.ToString());
            }
        }
          

        //Metodo para Atualizar Producto pasandoles como parametros todos los atributos
        //al que se requiera actualizar
        public async Task<string> ActualizarProducto([FromBody] Producto _Oproducto)
        {
            string extension = Path.GetExtension(_Oproducto.Fotografia.FileName);
            string nuevoNombreFoto = _Oproducto.NombreProducto + _Oproducto.Descripcion + extension;
            string _UbicacionFotografia = Path.Combine(_RutaArchivo, _Oproducto.Fotografia.FileName);
            try
            {
                using (FileStream FotografiaProducto = System.IO.File.Create(_UbicacionFotografia))
                {

                    _Oproducto.Fotografia.CopyTo(FotografiaProducto);
                    FotografiaProducto.Flush();
                }

                string _queryCommand = "actualizarProducto";
            var parametro = new DynamicParameters();
            parametro.Add("@ID_Producto", _Oproducto.ID_Producto, dbType: DbType.Int32);
            parametro.Add("@NombreProducto", _Oproducto.NombreProducto, dbType: DbType.String);
            parametro.Add("@Descripcion", _Oproducto.Descripcion, dbType: DbType.String);
            parametro.Add("@Stock", _Oproducto.Stock, dbType: DbType.Int32);
            parametro.Add("@Precio_Compra", _Oproducto.PrecioCompra, dbType: DbType.Double);
            parametro.Add("@Precio_Venta", _Oproducto.PrecioVenta, dbType: DbType.Double);
            parametro.Add("@IDCategoria", _Oproducto.ID_Categoria, dbType: DbType.Int32);
            parametro.Add("@imagen", _UbicacionFotografia, dbType: DbType.String);
            using (var con = new SqlConnection(_StringSql))
            {

                await con.ExecuteAsync(_queryCommand, parametro,
                    commandType: CommandType.StoredProcedure);
                return ("Producto Actualizado");
                }
            }
            catch (Exception ex)
            {
                return (ex.ToString());
            }
        }

        //metodo para eliminar Producto pasandole unicamente
        //el id que se desea eliminar
        public async Task<String> DeleteProducto(int id)
        {
            string _queryCommand = "eliminarProducto";
            var parametro = new DynamicParameters();
            parametro.Add("@ID_Producto", id, dbType: DbType.Int32);

            using (var con = new SqlConnection(_StringSql))
            {

                await con.ExecuteAsync(_queryCommand, parametro,
                    commandType: CommandType.StoredProcedure);
                return ("Producto Eliminado");
            }
        }
        //descargar fotografia       
          
        public (byte[] FileBytes, string MimeType, string FileName) ObtenerFotografia(string nombreArchivo)
        {
            string ubicacionArchivo = Path.Combine(_RutaArchivo, nombreArchivo);

            if (!System.IO.File.Exists(ubicacionArchivo))
            {
                throw new FileNotFoundException("El archivo no existe en la ubicación especificada.");
            }

            byte[] bytes = System.IO.File.ReadAllBytes(ubicacionArchivo);
            var base64String = Convert.ToBase64String(bytes);
            string mimeType = "application/octet-stream"; // Ajusta según el tipo de archivo si es necesario

            return (bytes, mimeType, nombreArchivo);
        }


    }
}
