namespace Employees.Client
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models;
    using Models.Dtos;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class EmployeeStartup
    {
        static void Main(string[] args)
        {
            ConfigureAutoMapping();

            SimpleMapping();
            AdvancedMapping();
            Projection();
        }

        private static void Projection()
        {
            Console.WriteLine("\nSolution to Problem 3. Projection");

            using (EmployeesContext context = new EmployeesContext())
            {
                Console.WriteLine("Initializing database [Employees]");
                context.Database.Initialize(true);

                List<EmployeeDto> employeeDtos = context.Employees
                    .Where(e => e.Birthday.Year < 1990)
                    .OrderByDescending(e => e.Salary)
                    .ProjectTo<EmployeeDto>()
                    .ToList();

                employeeDtos.ForEach(e => Console.WriteLine(e.ToStringWithManager()));
            }
        }

        private static void AdvancedMapping()
        {
            Console.WriteLine("\nSolution to Problem 2. Advanced Mapping");

            List<Employee> managers = SeedEmployeesWithManagers();
            List<ManagerDto> managerDtos = Mapper.Map<List<Employee>, List<ManagerDto>>(managers);

            managerDtos.ForEach(m => Console.WriteLine(m));
        }

        private static List<Employee> SeedEmployeesWithManagers()
        {
            // Add Employees / Managers
            Employee emp1 = new Employee()
            {
                FirstName = "Richard",
                LastName = "Wagner",
                Salary = 6000m,
                Birthday = new DateTime(1883, 2, 13)
            };
            Employee emp2 = new Employee()
            {
                FirstName = "Klingsor",
                LastName = "Thielemann",
                Salary = 1000m,
                Birthday = new DateTime(1960, 3, 25)
            };
            Employee emp3 = new Employee()
            {
                FirstName = "Gurnemanz",
                LastName = "Hotter",
                Salary = 2000m,
                Birthday = new DateTime(1990, 3, 25)
            };
            Employee emp4 = new Employee()
            {
                FirstName = "Amfortas",
                LastName = "Seilig",
                Salary = 3000m,
                Birthday = new DateTime(1980, 3, 25)
            };
            Employee emp5 = new Employee()
            {
                FirstName = "Parsifal",
                LastName = "Domingo",
                Salary = 4000m,
                Birthday = new DateTime(1989, 3, 25)
            };
            Employee emp6 = new Employee()
            {
                FirstName = "Kundry",
                LastName = "Meier",
                Salary = 5000m,
                Birthday = new DateTime(1975, 3, 25),
            };

            // Add Subordinates to Managers
            emp1.Subordinates = new[] { emp2, emp3, emp4, emp6 };
            emp6.Subordinates = new[] { emp5 };

            List<Employee> managers = new List<Employee>() { emp1, emp6 };

            return managers;
        }

        private static void SimpleMapping()
        {
            Console.WriteLine("Solution to Problem 1. Simple Mapping");

            Employee emp = new Employee()
            {
                FirstName = "Steve",
                LastName = "Jobs",
                Salary = 1m,
                Address = "Palo Alto",
                Birthday = new DateTime(1955, 2, 24)
            };
            EmployeeDto dto = Mapper.Map<EmployeeDto>(emp);

            Console.WriteLine($"{dto.FirstName} {dto.LastName} - {dto.Salary:f2}");
        }

        private static void ConfigureAutoMapping()
        {
            Mapper.Initialize(action =>
            {
                //v.1
                action.CreateMap<Employee, EmployeeDto>()
                    .ForMember(dto => dto.ManagerLastName, configExpression => configExpression.MapFrom(e => e.Manager.LastName));
                action.CreateMap<Employee, ManagerDto>()
                    .ForMember(dto => dto.SubordinatesCount, configExpression => configExpression.MapFrom(e => e.Subordinates.Count));

                // v.2 - auto mapping without configuration is also an option in this case
                //action.CreateMap<Employee, EmployeeDto>();
                //action.CreateMap<Employee, ManagerDto>();

                /* SubordinatesCount would be auto mapped to Subordinates.Count even without explicit mapping 
                 * A prop with a different name (e.g. NumberOfSubordinates) would require explicit configuration
                 * ManagerLastName would likewise be auto mapped to Manager.LastName even without explicti mapping config
                 */
            });
        }
    }
}
