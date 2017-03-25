namespace Employees.Models
{
    using System;
    using System.Collections.Generic;

    public class Employee
    {
        public Employee()
        {
            this.Subordinates = new HashSet<Employee>(); // Problem 2
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public bool IsOnHoliday { get; set; }           // Problem 2
        public int? ManagerId { get; set; }             // Problem 2
        public virtual Employee Manager { get; set; }   // Problem 2
        public virtual ICollection<Employee> Subordinates { get; set; } // Problem 2        
    }
}
