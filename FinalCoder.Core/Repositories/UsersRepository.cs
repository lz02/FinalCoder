using System;
using System.Collections.Generic;
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
                    $"INSERT INTO {TableName} (Nombre, Apellido, NombreUsuario, Contraseña, Mail) " +
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
                    $"SET Nombre = @name, Apellido = @surname, NombreUsuario = @username, Contraseña = @password, Mail = @email " +
                    $"WHERE Id = {user.ID}",
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
                    $"NombreUsuario = @username " +
                    $"AND Contraseña = @password", con);
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
                        throw new ArgumentException("Usuario o contraseña no validos!");
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
                    $"Mail = @mail " +
                    $"AND Contraseña = @password", con);
                command.Parameters.AddWithValue("@mail", email);
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
}
