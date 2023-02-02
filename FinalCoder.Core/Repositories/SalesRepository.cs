using System.Collections;
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

        public static IEnumerable<Sale> GetByUserId(long id)
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
