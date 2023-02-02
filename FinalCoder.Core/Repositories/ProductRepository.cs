using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FinalCoder.Core.Models;

namespace FinalCoder.Core.Repositories
{
    public static class UsersRepository
    {
        const string TableName = "Usuario";
        private static User MapToModel(SqlDataReader reader)
        {
            User user = new User();

            user.ID = reader.GetInt64(0);
            user.Name = reader.GetString(1);
            user.Surname = reader.GetString(2);
            user.UserName = reader.GetString(3);
            user.Password = reader.GetString(4);
            user.Email = reader.GetString(5);

            return user;
        }

        public static int Insert(User user)
        {
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand(
                    $"INSERT INTO {TableName} (Nombre, Apellido, NombreUsuario, Contraseña, Email) " +
                    $"VALUES (@name, @surname, @username, @password, @email)", con);

                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@surname", user.Surname);
                command.Parameters.AddWithValue("@username", user.UserName);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@email", user.Email);

                con.Open();
                return command.ExecuteNonQuery();
            }
        }
        public static int Update(User user)
        {
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand(
                    $"UPDATE {TableName} " +
                    $"SET Nombre = @name, Apellido = @surname, NombreUsuario = @username, Contraseña = @password, Email = @email",
                    con);

                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@surname", user.Surname);
                command.Parameters.AddWithValue("@username", user.UserName);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@email", user.Email);

                con.Open();
                return command.ExecuteNonQuery();
            }
        }
        public static int Delete(long id)
        {
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand($"DELETE FROM {TableName} WHERE Id = @id", con);
                command.Parameters.AddWithValue("@id", id);

                con.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
    public static class SalesRepository
    {
        const string TableName = "Venta";
        private static Sale MapToModel(SqlDataReader reader)
        {
            Sale sale = new Sale();

            sale.ID = reader.GetInt64(0);
            sale.Description = reader.GetString(1);
            sale.UserId = reader.GetInt64(2);

            return sale;
        }
    }
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
    }

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
                SqlCommand command = new SqlCommand(
                    $"INSERT INTO {TableName} (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) " +
                    $"VALUES (@desc, @cost, @price, @stock, @user)", con);

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
                SqlCommand command = new SqlCommand(
                    $"UPDATE {TableName} SET Descripciones = @desc, Costo = @cost, PrecioVenta = @price, Stock = @stock, IdUsuario = @user " +
                    $"WHERE Id = @id", con);

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
                SqlCommand command = new SqlCommand(
                    $"DELETE FROM {TableName} WHERE Id = @id",
                    con);

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
                SqlCommand command = new SqlCommand($"SELECT * FROM {TableName} WHERE Id = @id", con);
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
                SqlCommand command = new SqlCommand($"SELECT * FROM {TableName} WHERE Descripciones LIKE @description", con);
                command.Parameters.AddWithValue("@description", $"'{description}'");

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
                SqlCommand command = new SqlCommand($"SELECT * FROM {TableName}", con);

                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<Product> products = new List<Product>();
                    while (reader.Read())
                    {
                        products.Add(MapToModel(reader));
                    }
                    return products;
                }
            }
        }
        public static IEnumerable<Product> Search(string query)
        {
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM {TableName} WHERE Descripciones LIKE @query", con);
                command.Parameters.AddWithValue("@query", $"'%{query}%'");

                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<Product> products = new List<Product>();
                    while (reader.Read())
                    {
                        products.Add(MapToModel(reader));
                    }
                    return products;
                }
            }
        }
    }
}
