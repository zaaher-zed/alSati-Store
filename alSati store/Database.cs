
using System;
using Oracle.ManagedDataAccess.Client;

namespace alSati_store
{
    public class Database
    {
        private static readonly string connectionString =
            "User Id=Game_Store;" +
            "Password=aab;" +
            "Data Source=localhost:1521/XEPDB1;";

        public static OracleConnection GetConnection()
        {
            return new OracleConnection(connectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (OracleConnection connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
