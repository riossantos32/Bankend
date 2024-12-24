using Dapper;
using Distribuidora.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Distribuidora.Services
{
    public class ProveedorServices
    {
        
        private readonly IConfiguration _configuration;
        private readonly String _StringSql;

        public ProveedorServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _StringSql = _configuration.GetConnectionString("MyDB")!;
        }

        //Metodo para listar proveedor pasandole como parametro el id
        public async Task<Proveedores> GetProveedor(int id)
        {
            string _queryCommand = "listarProveedores";
            var parametro = new DynamicParameters();
            parametro.Add("@idProveedor", id, dbType: DbType.Int32);
            using (var con = new SqlConnection(_StringSql))
            {
                var _proveedor = await con.QueryFirstOrDefaultAsync<Proveedores>(
                    _queryCommand, parametro, commandType: CommandType.StoredProcedure);
                return _proveedor;
            }
        }
        //metodo para insertar Proveedor pasandoles como parametro los atributos
        // al que se le requiera insertar un valor
        public async Task<string> InsertarProveedor([FromBody] Proveedores _Oproveedor)
        {
            string _queryCommand = "insertarProveedor";
            var parametro = new DynamicParameters();
            parametro.Add("@Nombre", _Oproveedor.Nombre, dbType: DbType.String);
            parametro.Add("@Telefono", _Oproveedor.Telefono, dbType: DbType.String);
            parametro.Add("@Correo", _Oproveedor.Correo, dbType: DbType.String);
            parametro.Add("@Direccion", _Oproveedor.Direccion, dbType: DbType.String);

            using (var con = new SqlConnection(_StringSql))
            {

                await con.ExecuteAsync(_queryCommand, parametro,
                    commandType: CommandType.StoredProcedure);
                return ("Proveedor Insertada");
            }
        }
        //Metodo para Atualizar proveedor
        public async Task<string> ActualizarProveedor([FromBody] Proveedores _Oproveedor)
        {
            string _queryCommand = "actualizarProveedores";
            var parametro = new DynamicParameters();
            parametro.Add("@ID_Proveedores", _Oproveedor.ID_Proveedores, dbType: DbType.Int32);
            parametro.Add("@Nombre", _Oproveedor.Nombre, dbType: DbType.String);
            parametro.Add("@Telefono", _Oproveedor.Telefono, dbType: DbType.String);
            parametro.Add("@Correo", _Oproveedor.Correo, dbType: DbType.String);
            parametro.Add("@Direccion", _Oproveedor.Direccion, dbType: DbType.String);

            using (var con = new SqlConnection(_StringSql))
            {

                await con.ExecuteAsync(_queryCommand, parametro,
                    commandType: CommandType.StoredProcedure);
                return ("Proveedor Actualizado");
            }
        }
        //metodo para eliminar Proveedor pasandole unicamente
        //el id que se desea eliminar
        public async Task<string> DeleteProveedor(int id)
        {
            string _queryCommand = "eliminarProveedores";
            var parametro = new DynamicParameters();
            parametro.Add("@ID_Proveedor", id, dbType: DbType.Int32);

            using (var con = new SqlConnection(_StringSql))
            {

                await con.ExecuteAsync(_queryCommand, parametro,
                    commandType: CommandType.StoredProcedure);
                return ("Proveedor Eliminado");
            }
        }
    }
}
