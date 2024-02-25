using Microsoft.Data.SqlClient;

namespace ado_app.Data
{
    public class ConnectDB
    {
        public SqlConnection _connect;
        public ConnectDB()
        {
            _connect = new SqlConnection(Connection.ConnectionStr);
        }
    }
    public static class Connection
    {
        internal static string ConnectionStr { get; set; }
    }
}
