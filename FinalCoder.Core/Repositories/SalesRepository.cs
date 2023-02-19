using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FinalCoder.Core.Models;

namespace FinalCoder.Core.Repositories
{
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

        public static long Insert(Sale sale)
        {
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand(
                    $"INSERT INTO {TableName} (Comentarios, IdUsuario) " +
                    $"VALUES (@coms, @user); " +
                    $"SELECT @@IDENTITY", con);

                command.Parameters.AddWithValue("@coms", sale.Description);
                command.Parameters.AddWithValue("@user", sale.UserId);

                con.Open();
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public static int Delete(Sale sale)
        {
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand(
                    $"DELETE FROM {TableName} WHERE Id = @id",
                    con);

                command.Parameters.AddWithValue("@id", sale.ID);

                con.Open();
                return command.ExecuteNonQuery();
            }
        }

        public static Sale? GetById(long id)
        {
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM {TableName} " +
                    $"WHERE Id = @id", con);
                command.Parameters.AddWithValue("@id", id);

                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapToModel(reader);
                    }
                    return null;
                }
            }
        }
        public static IEnumerable<Sale> GetAll()
        {
            List<Sale> sales = new List<Sale>();
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM {TableName} ", con);

                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sales.Add(MapToModel(reader));
                    }
                }
            }
            return sales;
        }
        public static IEnumerable<Sale> GetAllByUser(long id)
        {
            List<Sale> sales = new List<Sale>();
            using (var con = Globals.SqlConnection)
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM {TableName} " +
                    $"WHERE IdUsuario = @id", con);
                command.Parameters.AddWithValue("@id", id);

                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sales.Add(MapToModel(reader));
                    }
                }
            }
            return sales;
        }
    }
}
