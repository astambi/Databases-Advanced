namespace Employees.Data.Migrations
{
    using Models;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<EmployeesContext> // Problem 3
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Employees.Data.EmployeesContext";
        }

        protected override void Seed(EmployeesContext context) // Problem 3
        {
            Console.WriteLine("Seeding data\n");

            SeedEmployees(context);
            AssignEmployeesToManagers(context);
        }

        private static void AssignEmployeesToManagers(EmployeesContext context)
        {
            context.Employees.Find(2).ManagerId = 1;
            context.Employees.Find(3).ManagerId = 1;
            context.Employees.Find(4).ManagerId = 1;
            context.Employees.Find(6).ManagerId = 1;
            context.Employees.Find(5).ManagerId = 6;
            context.SaveChanges();
        }

        private static void SeedEmployees(EmployeesContext context)
        {
            context.Employees.AddOrUpdate(e => new { e.FirstName, e.LastName },
                new Employee()
                {
                    FirstName = "Richard",
                    LastName = "Wagner",
                    Salary = 6000m,
                    Birthday = new DateTime(1883, 2, 13)
                },
                new Employee()
                {
                    FirstName = "Klingsor",
                    LastName = "Thielemann",
                    Salary = 1000m,
                    Birthday = new DateTime(1960, 3, 25)
                },
                new Employee()
                {
                    FirstName = "Gurnemanz",
                    LastName = "Hotter",
                    Salary = 2000m,
                    Birthday = new DateTime(1990, 3, 25)
                },
                new Employee()
                {
                    FirstName = "Amfortas",
                    LastName = "Seilig",
                    Salary = 3000m,
                    Birthday = new DateTime(1980, 3, 25)
                },
                new Employee()
                {
                    FirstName = "Parsifal",
                    LastName = "Domingo",
                    Salary = 4000m,
                    Birthday = new DateTime(1989, 3, 25)
                },
                new Employee()
                {
                    FirstName = "Kundry",
                    LastName = "Meier",
                    Salary = 5000m,
                    Birthday = new DateTime(1975, 3, 25),
                }
                );
            context.SaveChanges();
        }
    }
}
