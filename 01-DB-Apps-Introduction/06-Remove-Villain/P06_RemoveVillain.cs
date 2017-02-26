using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_Remove_Villain
{
    class P06_RemoveVillain
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection("Server=.;Database=MinionsDB;Integrated security=true");
            connection.Open();

            using (connection)
            {
                Console.Write("Enter villain ID: "); // for clarity, not required
                int villainId = int.Parse(Console.ReadLine());

                // check if villain exists in db
                SqlCommand getVillainNameCmd = GetSqlCommand(connection, "FindVillainName", villainId);
                string villainName = (string)getVillainNameCmd.ExecuteScalar();
                if (villainName == null)
                {
                    Console.WriteLine("No such villain was found"); return;
                }

                // delete villain froms MinionsVillains mapping table
                SqlCommand deleteMinionsVillainsCmd = GetSqlCommand(connection, "DeleteMinionsVillains", villainId);
                int realeasedMinions = (int)deleteMinionsVillainsCmd.ExecuteNonQuery();

                // delete villain from Villains
                SqlCommand deleteVillainCmd = GetSqlCommand(connection, "DeleteVillain", villainId);
                int affectedRows = (int)deleteVillainCmd.ExecuteNonQuery();
                
                if (affectedRows != 0)
                    Console.WriteLine($"{villainName} was deleted");
                Console.WriteLine($"{realeasedMinions} minions released");
            }
        }

        private static SqlCommand GetSqlCommand(SqlConnection connection, string fileName, int villainId)
        {
            string query = File.ReadAllText($@"../../{fileName}.sql");
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@villainId", villainId);
            return command;
        }
    }
}