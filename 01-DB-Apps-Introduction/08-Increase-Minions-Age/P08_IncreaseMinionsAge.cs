using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _08_Increase_Minions_Age
{
    class P08_IncreaseMinionsAge
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection("Server=.;Database=MinionsDB;Integrated security=true");
            connection.Open();

            using (connection)
            {
                UpdateMinionsNameAge(connection);
                PrintMinions(connection);
            }
        }

        private static void PrintMinions(SqlConnection connection)
        {
            string getNameAgeQuery = "SELECT Name, Age FROM Minions";
            SqlCommand getNameAgeCmd = new SqlCommand(getNameAgeQuery, connection);
            SqlDataReader reader = getNameAgeCmd.ExecuteReader();
            using (reader)
            {
                while (reader.Read())
                {
                    string name = (string)reader["Name"];
                    int age = (int)reader["Age"];
                    Console.WriteLine($"{name} {age}");
                }
            }
        }

        private static void UpdateMinionsNameAge(SqlConnection connection)
        {
            Console.Write("Enter minion IDs separated by space: "); // for clarity, not required
            int[] ids = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            for (int i = 0; i < ids.Length; i++)
            {
                // get minion's name
                SqlCommand getMinionCmd = GetSqlCommand(connection, "FindMinionName", ids[i]);
                string minionName = (string)getMinionCmd.ExecuteScalar();
                if (minionName != null)
                {
                    // convert name to titlecase
                    CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                    TextInfo textInfo = cultureInfo.TextInfo;
                    minionName = textInfo.ToTitleCase(minionName);
                    // update minion's name & age
                    SqlCommand updateMinionCmd = GetSqlCommand(connection, "UpdateMinion", ids[i]);
                    updateMinionCmd.Parameters.AddWithValue("@name", minionName);
                    updateMinionCmd.ExecuteNonQuery();
                }
            }
        }

        private static SqlCommand GetSqlCommand(SqlConnection connection, string fileName, int id)
        {
            string query = File.ReadAllText($@"../../{fileName}.sql");
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            return command;
        }
    }
}