using System.Data.SqlClient;

namespace FinalCoder.Core
{
    public static class CommandHelper
    {
        public static SqlCommand GetCommand(SqlConnection connection, string text)
            => new SqlCommand(text, connection);
    }
}
