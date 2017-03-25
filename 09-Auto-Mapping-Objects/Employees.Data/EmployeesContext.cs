namespace Employees.Data
{
    using Migrations;
    using Models;
    using System.Data.Entity;

    public class EmployeesContext : DbContext // Problem 3
    {        
        public EmployeesContext()
            : base("name=EmployeesContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EmployeesContext, Configuration>());
        }

        public virtual DbSet<Employee> Employees { get; set; }
    }
}