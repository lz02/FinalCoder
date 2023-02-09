using System;
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

        public static long Insert(ProductSale productSale)
        {
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand(
                    $"INSERT INTO {TableName} (Stock, IdProducto, IdVenta) " +
                    $"VALUES (@stock, @product, @sale) " +
                    $"SELECT @@IDENTITY", con);

                command.Parameters.AddWithValue("@stock", productSale.Stock);
                command.Parameters.AddWithValue("@product", productSale.ProductId);
                command.Parameters.AddWithValue("@sale", productSale.SaleId);

                con.Open();
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }

        public static ProductSale GetById(long id)
        {
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM ProductoVendido " +
                    $"WHERE Id = @id", con);
                command.Parameters.AddWithValue("@id", id);

                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    return MapToModel(reader);
                }
            }
        }
        /// <summary>
        /// Obtiene una liista con el detalle de una venta en particular.
        /// </summary>
        /// <param name="saleId">ID de la venta.</param>
        /// <returns>Una liista con el detalle de una venta en particular.</returns>
        public static IEnumerable<ProductSale> GetAllItemsOfSale(long saleId)
        {
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM ProductoVendido " +
                    $"INNER JOIN Venta " +
                    $"ON Venta.Id = ProductoVendido.IdVenta " +
                    $"WHERE Venta.Id = @id", con);
                command.Parameters.AddWithValue("@id", saleId);

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
        /// <summary>
        /// Obtiene una liista con el detalle de TODAS las ventas realizadas por un usuario especifico.
        /// </summary>
        /// <param name="usarId">ID del usuario a buscar.</param>
        /// <returns>Una liista con el detalle de las ventas realizadas por un usuario especifico.</returns>
        public static IEnumerable<ProductSale> GetAllByUser(long usarId)
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
