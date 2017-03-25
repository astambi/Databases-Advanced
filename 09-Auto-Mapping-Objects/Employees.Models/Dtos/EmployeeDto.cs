namespace Employees.Models
{
    public class EmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public string ManagerLastName { get; set; } // Problem 3

        public override string ToString() // Problem 2
        {
            return $"    - {this.FirstName} {this.LastName} {this.Salary:f2}";
        }

        public string ToStringWithManager() // Problem 3
        {
            return $"{this.FirstName} {this.LastName} {this.Salary:f2} - Manager: {this.ManagerLastName ?? "[no manager]"}";
        }
    }
}
