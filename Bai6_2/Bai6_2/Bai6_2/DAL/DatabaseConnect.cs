using System.Data.SqlClient;

namespace HRMManagementApp.DAL
{
    public class DatabaseConnect
    {
        private static readonly string _connectionString = @"Server=LAPTOP-F4JF2AS7\SQLEXPRESS;Database=HRMDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}