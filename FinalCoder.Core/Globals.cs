using System.Data.SqlClient;

namespace FinalCoder.Core
{
    public static class Globals
    {
        public static string _ConnectionString => "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SistemaGestion;Integrated Security=True";
        public static SqlConnection SqlConnection => new SqlConnection()
        {
            ConnectionString = _ConnectionString
        };
    }
}
