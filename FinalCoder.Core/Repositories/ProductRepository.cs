using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FinalCoder.Core.Models;

namespace FinalCoder.Core.Repositories
{
    public static class ProductRepository
    {
        const string TableName = "Producto";
        private static Product MapToModel(SqlDataReader reader)
        {
            Product product = new Product();

            product.ID = reader.GetInt64(0);
            product.Description = reader.GetString(1);
            product.Cost = reader.GetDecimal(2);
            product.SellPrice = reader.GetDecimal(3);
            product.Stock = reader.GetInt32(4);
            product.UserId = reader.GetInt64(5);

            return product;
        }

        public static int Insert(Product product)
        {
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = CommandHelper.GetCommand(con,
                    $"INSERT INTO {TableName} (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) " +
                    $"VALUES (@desc, @cost, @price, @stock, @user)");

                command.Parameters.AddWithValue("@desc", product.Description);
                command.Parameters.AddWithValue("@cost", product.Cost);
                command.Parameters.AddWithValue("@price", product.SellPrice);
                command.Parameters.AddWithValue("@stock", product.Stock);
                command.Parameters.AddWithValue("@user", product.UserId);

                con.Open();
                return command.ExecuteNonQuery();
            }
        }
        public static int Update(Product product)
        {
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = CommandHelper.GetCommand(con,
                    $"UPDATE {TableName} SET Descripciones = @desc, Costo = @cost, PrecioVenta = @price, Stock = @stock, IdUsuario = @user " +
                    $"WHERE Id = @id");

                command.Parameters.AddWithValue("@id", product.ID);
                command.Parameters.AddWithValue("@desc", product.Description);
                command.Parameters.AddWithValue("@cost", product.Cost);
                command.Parameters.AddWithValue("@price", product.SellPrice);
                command.Parameters.AddWithValue("@stock", product.Stock);
                command.Parameters.AddWithValue("@user", product.UserId);

                con.Open();
                return command.ExecuteNonQuery();
            }
        }
        public static int Delete(Product product)
        {
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = CommandHelper.GetCommand(con,
                    $"DELETE FROM {TableName} WHERE Id = @id");

                command.Parameters.AddWithValue("@id", product.ID);

                con.Open();
                return command.ExecuteNonQuery();
            }
        }

        public static Product GetById(long id)
        {
            Product product = new Product();
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = CommandHelper.GetCommand(con, $"SELECT * FROM {TableName} WHERE Id = @id");
                command.Parameters.AddWithValue("@id", id);

                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        product = MapToModel(reader);
                    }
                }
            }
            return product;
        }
        public static Product GetByDescription(string description)
        {
            Product product = new Product();
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = CommandHelper.GetCommand(con, $"SELECT * FROM {TableName} WHERE Descripciones = @description");
                command.Parameters.AddWithValue("@description", description);

                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        product = MapToModel(reader);
                    }
                }
            }
            return product;
        }
        public static IEnumerable<Product> GetAll()
        {
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = CommandHelper.GetCommand(con, $"SELECT * FROM {TableName}");

                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<Product> products = new List<Product>();
                    while (reader.Read())
                    {
                        products.Add(MapToModel(reader));
                    }
                    con.Close();
                    return products;
                }
            }
        }
    }
}
