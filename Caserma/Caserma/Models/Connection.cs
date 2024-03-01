using System.Configuration;
using System.Data.SqlClient;

namespace Caserma.Models
{
    public class Connection
    {
        //classe per la connessione al database
        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString.ToString();
            return conn;
        }
        public static SqlDataReader GetAdapter(string query, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            return cmd.ExecuteReader();
        }
    }
}