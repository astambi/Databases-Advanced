using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Initial_Setup
{
    class P01_InitialSetup
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection("Server=.;Integrated Security=true"); // db not yet created!
            connection.Open();

            using (connection)
            {
                CreateDB(connection);
                CreateDbTables(connection);
            }
        }

        private static void CreateDbTables(SqlConnection connection)
        {
            string query = File.ReadAllText(@"../../CreateMinionsDbTables.sql");
            SqlCommand command = new SqlCommand(query, connection);
            Console.WriteLine("Created DB tables. Rows affected {0}.", command.ExecuteNonQuery());
        }

        private static void CreateDB(SqlConnection connection)
        {
            string query = "CREATE DATABASE MinionsDB";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            Console.WriteLine("Created database MinionsDB.");
        }
    }
}