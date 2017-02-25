using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOdemo
{
    class ADOdemo
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(@"
                Server=.;
                Database=SoftUni;
                Integrated Security=true");
            connection.Open();

            using (connection)
            {
                //SqlCommand ExecuteScalar => single result
                string query = "SELECT COUNT(*) FROM Employees";
                SqlCommand command = new SqlCommand(query, connection);
                int employeesCount = (int)command.ExecuteScalar();

                Console.WriteLine("Employees count: {0}", employeesCount);

                //SqlDataReader => table, select
                Console.Write("Enter employeeId: ");
                int employeeId = int.Parse(Console.ReadLine());

                query = $@"SELECT EmployeeID, FirstName, LastName, Salary 
                           FROM Employees 
                           WHERE EmployeeId = {employeeId}";
                command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                        Console.Write($"{reader.GetName(i),-15}");
                    Console.WriteLine();

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                            Console.Write($"{reader[i],-15}");
                        Console.WriteLine();
                    }
                }

                // ExecuteNonQuery => insert, update, delete
                Console.Write("Enter townName to be inserted: ");
                string townName = Console.ReadLine();

                query = $"INSERT INTO Towns (Name) VALUES ('{townName}')";
                command = new SqlCommand(query, connection);
                int insertedRows = command.ExecuteNonQuery();

                Console.WriteLine($"Affected rows: {insertedRows}");

                // ExecuteNonQuery + Prevent SQL Injection
                Console.Write("Enter TownName to delete: ");
                string townNameToDel = Console.ReadLine();

                query = $"DELETE FROM Towns WHERE Name LIKE @townName";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@townName", "%" + townNameToDel + "%");
                int deletedRows = command.ExecuteNonQuery();

                Console.WriteLine($"Affected rows: {deletedRows}");
            }
        }
    }
}
