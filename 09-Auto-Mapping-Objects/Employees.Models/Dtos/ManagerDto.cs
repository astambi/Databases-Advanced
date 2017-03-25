namespace Employees.Models.Dtos
{
    using System.Collections.Generic;
    using System.Text;

    public class ManagerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<EmployeeDto> Subordinates { get; set; }
        public int SubordinatesCount { get; set; } // NB! would auto map to Subordinates.Count
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.FirstName} {this.LastName} | Employees: {this.SubordinatesCount}");
            foreach (EmployeeDto subordinate in this.Subordinates)
            {
                sb.AppendLine(subordinate.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}
