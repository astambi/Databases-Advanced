using Models;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SoftUniDB
{
    class SoftUniStartup
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Solutions to Advanced Querying - SoftUni database\n" +
                "Please import SoftUni database & if necessary modify the connection string\n");
            SoftUniContext context = new SoftUniContext();

            // Solutions
            CallAStoredProcedure(context);
            EmployeesMaxSalariesWithEF(context);
            EmployeesMaxSalariesWithNativeSQL(context);
        }

        private static void EmployeesMaxSalariesWithNativeSQL(SoftUniContext context)
        {
            Console.WriteLine("Solution to Problem. Max Salary by Department (using native SQL)\n");

            string sqlQueryMaxSalary = File.ReadAllText("../../SqlQueries/GetMaxSalaryByDepartment.sql");
            var maxSalaryByDepartments = context.Database.SqlQuery<DepartmentSalaryView>(sqlQueryMaxSalary);

            foreach (DepartmentSalaryView d in maxSalaryByDepartments)
            {
                Console.WriteLine($"{d.Name} - {d.MaxSalary:f2}");
            }
        }

        private static void EmployeesMaxSalariesWithEF(SoftUniContext context)
        {
            Console.WriteLine("Solution to Problem. Max Salary by Department (using EF & LINQ)\n");

            foreach (Department departement in context.Departments)
            {
                decimal maxSalary = context.Employees
                    .Where(e => e.DepartmentID == departement.DepartmentID)
                    .Max(e => e.Salary);
                if (maxSalary < 30000 || maxSalary > 70000)
                {
                    Console.WriteLine($"{departement.Name} - {maxSalary:f2}");
                }
            }
            Pause();
        }

        private static void CallAStoredProcedure(SoftUniContext context)
        {
            Console.WriteLine("Solution to Problem. Call a Stored Procedure\n");
            while (true)
            {
                Console.Write("1. Create a stored procedure for the first time (once only)\n" +
                    "2. Start querying (if the stored procedure is already created)\n" +
                    "Enter your choice or [end]: ");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1": CreateStoredProcedure(context); break;
                    case "2": ExecuteStoredProcedure(context); break;
                    case "end": break;
                    default: Console.WriteLine("Invalid option."); break;
                }
                if (option == "end" || option == "2") break;
            }
            Pause();
        }

        private static void ExecuteStoredProcedure(SoftUniContext context)
        {
            while (true)
            {
                Console.Write("Enter Employee [FirstName LastName] or [end]\ne.g. [Ruth Ellerbrock]: ");
                string input = Console.ReadLine();
                if (input == "end") break;

                string[] names = Regex.Split(input, @"\s+");
                if (names.Length == 2)
                {
                    string firstName = names[0];
                    string lastName = names[1];
                    var projects = context.Database
                        .SqlQuery<ProjectViewModel>($"EXEC dbo.usp_GetProjectsByEmployee {firstName}, {lastName}");
                    if (projects.Count() > 0)
                    {
                        Console.WriteLine($"\nListing projects belonging to {firstName} {lastName}:\n");
                        foreach (ProjectViewModel p in projects)
                        {
                            Console.WriteLine($"{p.Name} - {p.Description}, {p.StartDate}");
                            Console.WriteLine(new string('*', Console.WindowWidth - 1));
                        }
                    }
                    else Console.WriteLine($"{firstName} {lastName} does not have any projects");
                }
                else Console.WriteLine("Invalid number of arguments");
            }
        }

        private static void CreateStoredProcedure(SoftUniContext context)
        {
            Console.WriteLine("Creating Stored Procedure [usp_GetProjectsByEmployee]");
            string sqlQueryCreateProc = File.ReadAllText(@"../../SqlQueries/CreateProc_GetProjectsByEmployee.sql");
            context.Database.ExecuteSqlCommand(sqlQueryCreateProc);
        }

        private static void Pause()
        {
            Console.WriteLine("\nPress any key to continue with the next problem");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
