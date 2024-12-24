    using Distribuidora.Modelo;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Dapper;


namespace Distribuidora.Services
{
    public class ClienteServices
    {
        private readonly IConfiguration _configuration;
        private readonly String _StringSql;

        public ClienteServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _StringSql = _configuration.GetConnectionString("MyDB")!;
        }

        //Metodo para listar Cliente pasandole como parametro el id
        public async Task<Cliente> GetCliente(int id)
        {
                string _queryCommand = "listarClientes";
                var parametro = new DynamicParameters();
                parametro.Add("@P_idCliente", id, dbType: DbType.Int32);
                using (var con = new SqlConnection(_StringSql))
                {
                    var _cliente = await con.QueryFirstOrDefaultAsync<Cliente>(
                        _queryCommand, parametro, commandType: CommandType.StoredProcedure);
                    return _cliente;
                }
            }
        
        
        //metodo para insertar Cliente pasandoles como parametro los atributos
        // al que se le requiera insertar un valor
        public async Task<string> InsertarCliente([FromBody] Cliente _Ocliente)
        {
            string _queryCommand = "insertarClientes";
            var parametro = new DynamicParameters();
            parametro.Add("@Nombre", _Ocliente.Nombre, dbType: DbType.String);
            parametro.Add("@Apellido", _Ocliente.Apellido, dbType: DbType.String);
            parametro.Add("@Tipo_Cliente", _Ocliente.Tipo_Cliente, dbType: DbType.String);
            parametro.Add("@Email", _Ocliente.Email, dbType: DbType.String);
            parametro.Add("@contraseña", _Ocliente.Contraseña, dbType: DbType.String);

            using (var con = new SqlConnection(_StringSql))
            {

                await con.ExecuteAsync(_queryCommand, parametro,
                    commandType: CommandType.StoredProcedure);
                return ("Cliente Insertado");
            }
        }
        //Metodo para Atualizar Cliente pasandoles como parametros todos los atributos
        //al que se requiera actualizar
        public async Task<string> ActualizarCliente([FromBody] Cliente _Ocliente)
        {
          

            string _queryCommand = "actualizarClientes";
            var parametro = new DynamicParameters();
            parametro.Add("@ID_Cliente", _Ocliente.ID_Cliente, dbType: DbType.Int32);
            parametro.Add("@Nombre", _Ocliente.Nombre, dbType: DbType.String);
            parametro.Add("@Apellido", _Ocliente.Apellido, dbType: DbType.String);
            parametro.Add("@Tipo_Cliente", _Ocliente.Tipo_Cliente, dbType: DbType.String);
            parametro.Add("@Email", _Ocliente.Email, dbType: DbType.String);
            parametro.Add("@contraseña", _Ocliente.Contraseña, dbType: DbType.String);

            using (var con = new SqlConnection(_StringSql))
            {

                await con.ExecuteAsync(_queryCommand, parametro,
                    commandType: CommandType.StoredProcedure);
                return ("Cliente Actualizado");
            }
        }

        //metodo para eliminar Cliente pasandole unicamente
        //el id que se desea eliminar
        public async Task<string> DeleteCliente(int id)
        {
            string _queryCommand = "eliminarClientes";
            var parametro = new DynamicParameters();
            parametro.Add("@ID_Cliente", id, dbType: DbType.Int32);

            using (var con = new SqlConnection(_StringSql))
            {

                await con.ExecuteAsync(_queryCommand, parametro,
                    commandType: CommandType.StoredProcedure);
                return ("Cliente Eliminado");
            }
        }
    }
}
    
   