using Dapper;
using Distribuidora.Modelo;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;


namespace Distribuidora.Services
{
    public class CategoriaServices
    {
        private readonly IConfiguration _configuration;
        private readonly String _StringSql;

        public CategoriaServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _StringSql = _configuration.GetConnectionString("MyDB")!;
        }

        //Metodo para listar Categoria pasandole como parametro el id
        public async Task<Categoria> GetCategoria(int id)
        {

            string _queryCommand = "listarCategoria";
            var parametro = new DynamicParameters();
            parametro.Add("@P_id", id, dbType: DbType.Int32);
            using (var con = new SqlConnection(_StringSql))
            {
                var _categoria = await con.QueryFirstOrDefaultAsync<Categoria>(
                    _queryCommand, parametro, commandType: CommandType.StoredProcedure);
                return _categoria;
            }
        }

        //metodo para insertar Categoria pasandoles como parametro los atributos
        // al que se le requiera insertar un valor
        public async Task<string> InsertarCategoria([FromBody] Categoria _Ocategoria)
        {
            string _queryCommand = "insertarCategoria";
            var parametro = new DynamicParameters();
            parametro.Add("@nombreCategoria", _Ocategoria.nombreCategoria, dbType: DbType.String);
            
            using (var con = new SqlConnection(_StringSql))
            {

                await con.ExecuteAsync(_queryCommand, parametro,
                    commandType: CommandType.StoredProcedure);
                return ("Categoria Insertada");
            }
        }

        //Metodo para Atualizar categoria pasandoles como parametros todos los atributos
        //al que se requiera actualizar
        public async Task<string> ActualizarCategoria([FromBody] Categoria _Oestudiante)
        {
            string _queryCommand = "actualizarCategoria";
            var parametro = new DynamicParameters();
            parametro.Add("@ID_Categoria", _Oestudiante.ID_Categoria, dbType: DbType.Int32);
            parametro.Add("@NombreCategoria", _Oestudiante.nombreCategoria, dbType: DbType.String);
           
            using (var con = new SqlConnection(_StringSql))
            {

                await con.ExecuteAsync(_queryCommand, parametro,
                    commandType: CommandType.StoredProcedure);
                return ("Categoria Actualizada");
            }
        }

        //metodo para eliminar categoria pasandole unicamente
        //el id que se desea eliminar

        public async Task<string> DeleteCategoria(int id)
        {
            string _queryCommand = "eliminarCategoria";
            var parametro = new DynamicParameters();
            parametro.Add("@Categoria", id, dbType: DbType.Int32);

            using (var con = new SqlConnection(_StringSql))
            {

                await con.ExecuteAsync(_queryCommand, parametro,
                    commandType: CommandType.StoredProcedure);
                return ("Categoria Eliminado");
            }
        }
    }
}
    
    




