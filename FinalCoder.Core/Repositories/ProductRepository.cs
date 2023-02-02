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

        public static User GetById(long id)
        {
            User user = new User();
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM {TableName} WHERE Id = @id", con);
                command.Parameters.AddWithValue("@id", id);

                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = MapToModel(reader);
                    }
                }
            }
            return user;
        }
        public static User GetByUsername(string username)
        {
            User user = new User();
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM {TableName} WHERE NombreUsuario = @username", con);
                command.Parameters.AddWithValue("@username", username);

                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = MapToModel(reader);
                    }
                }
            }
            return user;
        }
        public static IEnumerable<User> GetAll()
        {
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM {TableName}", con);

                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<User> users = new List<User>();
                    while (reader.Read())
                    {
                        users.Add(MapToModel(reader));
                    }
                    return users;
                }
            }
        }

        public static User LoginWithUsername(string username, string password)
        {
            User user = new User();
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM {TableName} WHERE " +
                    $"NombreUsuario = @username" +
                    $"Contraseña = @password", con);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = MapToModel(reader);
                    }
                    else
                    {
                        throw new InvalidOperationException("Usuario o contraseña no validos!");
                    }
                }
            }
            return user;
        }
        public static User LoginWithEmail(string email, string password)
        {
            User user = new User();
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM {TableName} WHERE " +
                    $"Email = @emial" +
                    $"Contraseña = @password", con);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", password);

                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = MapToModel(reader);
                    }
                    else
                    {
                        throw new InvalidOperationException("Email o contraseña no validos!");
                    }
                }
            }
            return user;
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

        public static Sale GetByUserId(long id)
        {
            Sale sale = new Sale();
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM {TableName} " +
                    $"WHERE IdUsuario = @id", con);
                command.Parameters.AddWithValue("@id", id);

                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sale = MapToModel(reader);
                    }
                }
            }
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

        public static IEnumerable<ProductSale> GetAllByUser(long usarId)
        {
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM Producto " +
                    $"INNER JOIN ProductoVendido " +
                    $"ON ProductoVendido.IdUsuario = @id", con);
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

    public static class ProductsRepository
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

        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto para buscar.</param>
        /// <returns>Un producto cuya ID coincide con el <paramref name="id"/> indicado.</returns>
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
        /// <summary>
        /// Obtiene un producto por su nombre.
        /// </summary>
        /// <param name="name">Nombre del producto para buscar.</param>
        /// <returns>Un producto cuya Descripción coincide con el <paramref name="name"/> indicado.</returns>
        public static Product GetByName(string name)
        {
            Product product = new Product();
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM {TableName} WHERE Descripciones LIKE @description", con);
                command.Parameters.AddWithValue("@description", $"'{name}'");

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
        /// <summary>
        /// Obtiene todos los registros disponibles.
        /// </summary>
        /// <returns>Una lista con todos los productos en la base de datos.</returns>
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
        /// <summary>
        /// Obtiene todos los productos cargados por un usuario específico.
        /// </summary>
        /// <param name="usarId">ID del usuario para buscar.</param>
        /// <returns>Una lista con todos los productos en la base de datos, cargados por un usuario específico.</returns>
        public static IEnumerable<Product> GetAllByUser(long usarId)
        {
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM {TableName} " +
                    $"WHERE IdUsuario = @id", con);
                command.Parameters.AddWithValue("@id", usarId);

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
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM {TableName} " +
                    $"WHERE Descripciones LIKE @query", con);
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
