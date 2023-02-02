using System.Collections.Generic;
using System.Data.SqlClient;
using FinalCoder.Core.Models;

namespace FinalCoder.Core.Repositories
{
    public static class ProductSalesRepository
    {
        const string TableName = "ProductoVendido";
        private static ProductSale MapToModel(SqlDataReader reader)
        {
            ProductSale productSale = new ProductSale();

            productSale.ID = reader.GetInt64(0);
            productSale.Stock = reader.GetInt32(1);
            productSale.ProductId = reader.GetInt64(2);
            productSale.SaleId = reader.GetInt64(3);

            return productSale;
        }

        /// <summary>
        /// Obtiene una liista con el detalle de las ventas realizadas por un usuario especifico.
        /// </summary>
        /// <param name="usarId">ID del usuario a buscar.</param>
        /// <returns>Una liista con el detalle de las ventas realizadas por un usuario especifico.</returns>
        public static IEnumerable<ProductSale> GetAllSalesByUser(long usarId)
        {
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM ProductoVendido " +
                    $"INNER JOIN Venta " +
                    $"ON Venta.Id = ProductoVendido.IdVenta " +
                    $"WHERE Venta.IdUsuario = @id", con);
                command.Parameters.AddWithValue("@id", usarId);

                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<ProductSale> productSales = new List<ProductSale>();
                    while (reader.Read())
                    {
                        productSales.Add(MapToModel(reader));
                    }
                    return productSales;
                }
            }
        }
    }
}
