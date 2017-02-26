using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Get_Villains_Names
{
    class P02_GetVillainsNames
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection("Server =.;Database=MinionsDB;Integrated Security = true");
            connection.Open();

            using (connection)
            {
                string query = File.ReadAllText(@"../../FindVillainsCounts.sql");
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        string villainName = (string)reader["Name"];
                        int numberOfMinions = (int)reader["NumberOfMinions"];
                        Console.WriteLine($"{villainName} {numberOfMinions}");
                    }
                }
            }
        }
    }
}