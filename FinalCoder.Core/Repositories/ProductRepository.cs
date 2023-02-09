using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FinalCoder.Core.Models;

namespace FinalCoder.Core.Repositories
{
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
        private static bool IsValid(Product product)
        {
            if (string.IsNullOrEmpty(product.Description) || product.SellPrice == 0 || product.Stock < 0 || product.UserId == 0)
            {
                return false;
            }
            return true;
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

                if (IsValid(product))
                {
                    return command.ExecuteNonQuery();
                }

                throw new ArgumentException();
            }
        }
        public static int Update(Product product)
        {
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand(
                    $"UPDATE {TableName} SET Descripciones = @desc, Costo = @cost, PrecioVenta = @price, Stock = @stock, IdUsuario = @user " +
                    $"WHERE Id = @id ", con);

                command.Parameters.AddWithValue("@id", product.ID);
                command.Parameters.AddWithValue("@desc", product.Description);
                command.Parameters.AddWithValue("@cost", product.Cost);
                command.Parameters.AddWithValue("@price", product.SellPrice);
                command.Parameters.AddWithValue("@stock", product.Stock);
                command.Parameters.AddWithValue("@user", product.UserId);

                con.Open();

                if (IsValid(product))
                {
                    return command.ExecuteNonQuery();
                }

                throw new ArgumentException();
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

        /// <summary>
        /// Obtiene una lista con los productos vendidos por un usuario especifico.
        /// </summary>
        /// <param name="usarId">ID del usuario a buscar.</param>
        /// <returns>Una lista con todos los productos que figuran 
        /// en Ventas realizadas por un usuario especifico.</returns>
        public static IEnumerable<Product> GetAllSalesByUser(long usarId)
        {
            IEnumerable<ProductSale> sales = ProductSalesRepository.GetAllByUser(usarId);
            using (var con = Globals.SqlConnection)
            {
                List<Product> products = new List<Product>();
                con.Open();
                foreach (ProductSale sale in sales)
                {
                    SqlCommand command = new SqlCommand($"SELECT * FROM {TableName} WHERE Id = @id", con);
                    command.Parameters.AddWithValue("@id", sale.ProductId);
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            products.Add(MapToModel(reader));
                        }
                    }
                }
                return products;
            }
        }
    }
}
