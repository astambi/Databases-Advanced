using System.ComponentModel.DataAnnotations;

namespace _01_Student_System.Models
{
    public class License // Problem 4
    {
        public License()
        {
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int ResourceId { get; set; }

        public virtual Resource Resource { get; set; }
    }
}
