using Dapper;
using Distribuidora.Modelo;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;


namespace Distribuidora.Services
{
    public class MaestroVentaServices
    {
        private readonly IConfiguration _configuration;
        private readonly String _StringSql;

        public MaestroVentaServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _StringSql = _configuration.GetConnectionString("MyDB")!;
        }

        public async Task<string> InsertarVentaFactura([FromBody] VentaFactura _Ofactura)
        {
            string _queryCommand = "insertarVenta_factura";
            var parametro = new DynamicParameters();
            parametro.Add("@ID_Cliente", _Ofactura.idCliente, dbType: DbType.Int32);
            parametro.Add("@Fecha", _Ofactura.Fecha, dbType: DbType.DateTime);

            using (var con = new SqlConnection(_StringSql))
            {

                await con.ExecuteAsync(_queryCommand, parametro,
                    commandType: CommandType.StoredProcedure);
                return ("Venta Insertado");
            }
        }

        public async Task<String> ObtenerUltimoNumeroFactura()
        {
            string _queryCommand = "obtenerUltimoIDFactura";


            using (var con = new SqlConnection(_StringSql))
            {
                await con.ExecuteAsync(_queryCommand,
                       commandType: CommandType.StoredProcedure);
                return ("venta Insertado");
            }
        }

        public async Task<string> InsertarDetalleVentaFactura([FromBody] MaestroDetalle_venta _Odetalle)
        {

            string _queryCommand = "insertarDetalleVenta";
            var parametros = new DynamicParameters();

            parametros.Add("@IDProducto", _Odetalle.IDProducto, DbType.Int32);
            parametros.Add("@Cantidad", _Odetalle.Cantidad, DbType.Int32);
            parametros.Add("@PrecioVenta", _Odetalle.precioVenta, DbType.Double);
            parametros.Add("@NumeroFactura", _Odetalle.NumeroFactura, DbType.Int32);
            parametros.Add("@Total", _Odetalle.total, DbType.Int32);


            using (var con = new SqlConnection(_StringSql))
            {
                await con.ExecuteAsync(_queryCommand, parametros,
                  commandType: CommandType.StoredProcedure);
                return ("DetalleVenta Insertado");
            }
        }

    }
}
    
   