using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_Add_Minion
{
    class P04_AddMinion
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection("Server=.;Database=MinionsDB;Integrated security=true");
            connection.Open();            

            using (connection)
            {
                Console.WriteLine("Enter minion & villain inputs: "); // for clarity, not required
                string[] minionInput = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] villainInput = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string minionName = minionInput[1];
                int minionAge = int.Parse(minionInput[2]);
                string minionTownName = minionInput[3];
                string villainName = villainInput[1];

                // add town/ minion/ villain / minion-villain if not already in DB, otherwise get their ids
                int townId = AddTownReturnId(connection, minionTownName);
                int minionId = AddMinionReturnId(connection, minionName, minionAge, townId);
                int villainId = AddVillainReturnId(connection, villainName);
                AddMinionVillain(connection, minionName, minionId, villainName, villainId);
            }
        }

        private static void AddMinionVillain(SqlConnection connection, string minionName, int minionId, string villainName, int villainId)
        {
            SqlCommand searchMinionVillainCmd = GetSqlCommand(connection, "FindMinionVillain", "@minionId", minionId.ToString());
            searchMinionVillainCmd.Parameters.AddWithValue("@villainId", villainId);
            int count = (int?)searchMinionVillainCmd.ExecuteScalar() ?? 0;

            while (count == 0)
            {
                SqlCommand addMinionVillainCmd = GetSqlCommand(connection, "AddMinionVillain", "@minionId", minionId.ToString());
                addMinionVillainCmd.Parameters.AddWithValue("@villainId", villainId.ToString());
                int affectedRows = (int)addMinionVillainCmd.ExecuteNonQuery();
                if (affectedRows != 0)
                    Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");

                count = (int?)searchMinionVillainCmd.ExecuteScalar() ?? 0;
            }
        }

        private static int AddVillainReturnId(SqlConnection connection, string name)
        {
            SqlCommand searchVillainCmd = GetSqlCommand(connection, "FindVillain", "@name", name);
            int id = (int?)searchVillainCmd.ExecuteScalar() ?? -1;

            while (id == -1)
            {
                SqlCommand addVillainCmd = GetSqlCommand(connection, "AddVillain", "@name", name);
                int affectedRows = (int)addVillainCmd.ExecuteNonQuery();
                if (affectedRows != 0)
                    Console.WriteLine($"Villain {name} was added to the database.");

                id = (int?)searchVillainCmd.ExecuteScalar() ?? -1;
            }
            return id;
        }

        private static int AddMinionReturnId(SqlConnection connection, string name, int age, int townId)
        {
            SqlCommand searchMinionCmd = GetSqlCommand(connection, "FindMinion", "@name", name);
            int id = (int?)searchMinionCmd.ExecuteScalar() ?? -1;
            while (id == -1)
            {
                SqlCommand addMinionCmd = GetSqlCommand(connection, "AddMinion", "@name", name);
                addMinionCmd.Parameters.AddWithValue("@age", age);
                addMinionCmd.Parameters.AddWithValue("@townId", townId);
                addMinionCmd.ExecuteNonQuery();

                id = (int?)searchMinionCmd.ExecuteScalar() ?? -1;
            }
            return id;
        }

        private static int AddTownReturnId(SqlConnection connection, string name)
        {
            SqlCommand searchTownCmd = GetSqlCommand(connection, "FindTown", "@name", name);
            int id = (int?)searchTownCmd.ExecuteScalar() ?? -1;
            while (id == -1)
            {
                SqlCommand addTownCmd = GetSqlCommand(connection, "AddTown", "@name", name);
                int affectedRows = (int)addTownCmd.ExecuteNonQuery();
                if (affectedRows != 0)
                    Console.WriteLine($"Town {name} was added to the database.");

                id = (int?)searchTownCmd.ExecuteScalar() ?? -1;
            }
            return id;
        }

        private static SqlCommand GetSqlCommand(SqlConnection connection, string fileName, string paramName, string paramValue)
        {
            string query = File.ReadAllText($@"../../{fileName}.sql");
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue(paramName, paramValue);
            return command;
        }
    }
}