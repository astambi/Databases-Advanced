using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises_SoftUni
{
    class Exercises_SoftUni
    {
        static void Main(string[] args)
        {
            SoftuniContext context = new SoftuniContext();

            P03_EmployeesFullInformation(context);
            P04_EmployeesWithSalaryOver50000(context);
            P05_EmployeesFromRD(context);
            P06_AddNewAddressUpdateEmployee(context);
            P07_FindEmployeesInPeriod(context);
            P08_AddressesByTownName(context);
            P09_EmployeeId147(context);
            P10_DepartmentsWithMoreThan5Employees(context);
            P11_FindLatest10Projects(context);
            P12_IncreaseSalaries(context);
            P13_FindEmployeesByFirstNameStartingWithSA(context);
            P15_DeleteProjectById(context);
        }

        private static void P15_DeleteProjectById(SoftuniContext context)
        {
            Project targetProject = context.Projects.Find(2);
            if (targetProject != null)
            {
                foreach (Employee employee in targetProject.Employees)
                    employee.Projects.Remove(targetProject);
                context.Projects.Remove(targetProject);
                context.SaveChanges();
            }
            var projects = context.Projects.Select(p => p.Name).Take(10);
            foreach (var p in projects)
                Console.WriteLine(p);
        }

        private static void P13_FindEmployeesByFirstNameStartingWithSA(SoftuniContext context)
        {
            var employees = context.Employees.Where(e => e.FirstName.StartsWith("SA"));
            foreach (Employee e in employees)
                Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f4})");
        }

        private static void P12_IncreaseSalaries(SoftuniContext context)
        {
            string[] departmentNames = new[] { "Engineering", "Tool Design", "Marketing", "Information Services" };
            var employees = context.Employees
                .Where(e => departmentNames.Contains(e.Department.Name));
            foreach (Employee e in employees)
                e.Salary *= 1.12m;
            context.SaveChanges();
            foreach (Employee e in employees)
                Console.WriteLine($"{e.FirstName} {e.LastName} (${e.Salary:f6})");
        }

        private static void P11_FindLatest10Projects(SoftuniContext context)
        {
            var projects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .OrderBy(p => p.Name);
            foreach (Project p in projects)
                Console.WriteLine($"{p.Name} {p.Description} {p.StartDate:M/d/yyyy h:mm:ss tt}");
        }

        private static void P10_DepartmentsWithMoreThan5Employees(SoftuniContext context)
        {
            var departments = context.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count);
            foreach (Department d in departments)
            {
                Console.WriteLine($"{d.Name} {d.Manager.FirstName}");
                foreach (Employee e in d.Employees)
                    Console.WriteLine($"{e.FirstName} {e.LastName} {e.JobTitle}");
            }
        }

        private static void P09_EmployeeId147(SoftuniContext context)
        {
            Employee employee = context.Employees.FirstOrDefault(e => e.EmployeeID == 147);
            Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");
            foreach (Project p in employee.Projects.OrderBy(p => p.Name))
                Console.WriteLine($"{p.Name}");
        }

        private static void P08_AddressesByTownName(SoftuniContext context)
        {
            var addresses = context.Addresses
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(a => a.Town.Name)
                .Take(10);
            foreach (Address a in addresses)
                Console.WriteLine($"{a.AddressText}, {a.Town.Name} - {a.Employees.Count()} employees");
        }

        private static void P07_FindEmployeesInPeriod(SoftuniContext context)
        {
            var employees = context.Employees
                .Where(e => e.Projects.Count(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003) > 0)
                .Take(30);
            foreach (Employee e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} {e.Manager.FirstName}");
                foreach (Project p in e.Projects)
                    Console.WriteLine($"--{p.Name} {p.StartDate:M/d/yyyy h:mm:ss tt} {p.EndDate:M/d/yyyy h:mm:ss tt}");
            }
        }

        private static void P06_AddNewAddressUpdateEmployee(SoftuniContext context)
        {
            Address address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownID = 4
            };
            Employee employee = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");
            if (employee != null)
            {
                employee.Address = address;
                context.SaveChanges();
            }
            var employees = context.Employees
                .OrderByDescending(e => e.AddressID)
                .Take(10)
                .Select(e => e.Address.AddressText);
            foreach (var addressText in employees)
                Console.WriteLine(addressText);
        }

        private static void P05_EmployeesFromRD(SoftuniContext context)
        {
            var employees = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName);
            foreach (Employee e in employees)
                Console.WriteLine($"{e.FirstName} {e.LastName} from {e.Department.Name} - ${e.Salary:f2}");
        }

        private static void P04_EmployeesWithSalaryOver50000(SoftuniContext context)
        {
            var employees = context.Employees
                .Where(e => e.Salary > 50000)
                .Select(e => e.FirstName);
            foreach (string e in employees)
                Console.WriteLine(e);
        }

        private static void P03_EmployeesFullInformation(SoftuniContext context)
        {
            List<Employee> employees = context.Employees.OrderBy(e => e.EmployeeID).ToList();
            foreach (Employee e in employees)
                Console.WriteLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary}");
        }
    }
}
