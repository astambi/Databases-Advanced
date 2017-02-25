using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    class Examples
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection("Server=.;Database=SoftUni;Integrated Security=true");
            connection.Open();

            using (connection)
            {
                while (true)
                {
                    Console.Write("Enter command [list / details / search / searchALL / exit]: ");
                    string command = Console.ReadLine();
                    Console.Clear();

                    switch (command)
                    {
                        case "list":
                            ListAllProjects(connection); break;
                        case "details":
                            ShowProjectDetails(connection); break;
                        case "search":
                            SearchByNameFirst(connection); break;
                        case "searchALL":
                            SearchByNameAll(connection); break;
                        case "exit": return;
                    }
                }
            }
        }

        private static void ListAllProjects(SqlConnection connection)
        {
            string query = @"SELECT ProjectID, Name FROM Projects";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();

            using (reader)
            {
                Console.WriteLine("  ID | ProjectName");
                Console.WriteLine("---- + --------------");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0],4} | {reader[1]}");
                }
            }
        }

        private static void ShowProjectDetails(SqlConnection connection)
        {
            Console.Write("Enter ProjectID: ");
            int projectID = int.Parse(Console.ReadLine());

            string queryProjectDetails = @"
                SELECT * FROM Projects 
                WHERE ProjectID = @ProjectID";
            SqlCommand cmd = new SqlCommand(queryProjectDetails, connection);
            cmd.Parameters.AddWithValue("@ProjectID", projectID);
            SqlDataReader reader = cmd.ExecuteReader();

            // project details
            using (reader)
            {
                if (!reader.Read())
                {
                    Console.WriteLine($"No project with ID {projectID} found");
                    return;
                }
                Console.WriteLine("::::::::::::::::::::::::::::::::::::::");
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.WriteLine($"{reader.GetName(i),-15} : {reader[i]}");
                }
            }
            // project employees
            string queryEmployees = @"
                SELECT e.EmployeeID, e.FirstName, e.LastName
                FROM EmployeesProjects AS pe 
                LEFT JOIN Employees AS e ON pe.EmployeeID = e.EmployeeID
                WHERE pe.ProjectID = @ProjectID";
            SqlCommand cmdEmpl = new SqlCommand(queryEmployees, connection);
            cmdEmpl.Parameters.AddWithValue("@ProjectID", projectID);
            SqlDataReader readerEmpl = cmdEmpl.ExecuteReader();

            Console.WriteLine("::::::::::::::::::::::::::::::::::::::");
            Console.WriteLine("Assigned employees:");

            using (readerEmpl)
            {
                if (!readerEmpl.HasRows)
                { Console.WriteLine("No employees"); }
                else
                {
                    while (readerEmpl.Read())
                    {
                        Console.WriteLine($"{readerEmpl[0],15} : {readerEmpl[1]} {readerEmpl[2]}");
                    }
                }
                Console.WriteLine("::::::::::::::::::::::::::::::::::::::");
            }
        }

        private static void SearchByNameAll(SqlConnection connection)
        {
            Console.Write("Enter project name: ");
            string pattern = Console.ReadLine();

            string query = @"
                SELECT ProjectID FROM Projects 
                WHERE Name LIKE @ProjectName";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ProjectName", "%" + pattern + "%");
            SqlDataReader reader = cmd.ExecuteReader();

            using (reader)
            {
                if (!reader.HasRows)
                {
                    Console.WriteLine("Project not found.");
                    return;
                }
                List<int> projectIDs = new List<int>();
                while (reader.Read())
                {
                    projectIDs.Add((int)reader[0]);
                }
                Console.WriteLine("Found projects with IDs: {0}", String.Join(", ", projectIDs));
            }
        }

        private static void SearchByNameFirst(SqlConnection connection)
        {
            Console.Write("Enter project name: ");
            string pattern = Console.ReadLine();

            string query = @"
                SELECT TOP 1 ProjectID FROM Projects 
                WHERE Name LIKE @ProjectName";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ProjectName", "%" + pattern + "%");
            int? projectID = (int?)cmd.ExecuteScalar() ?? -1; // NB! nullable with default value if null

            if (projectID == -1)
            {
                Console.WriteLine("Project not found.");
                return;
            }
            Console.WriteLine($"First project found: ID {projectID}");
        }
    }
}
