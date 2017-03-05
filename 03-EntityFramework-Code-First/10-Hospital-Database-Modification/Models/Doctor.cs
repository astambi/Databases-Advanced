using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10_Hospital_Database_Modification.Models
{
    public class Doctor
    {
        private string name;
        private string specialty;

        public Doctor()
        {
            this.Visitations = new HashSet<Visitation>();
        }
        public Doctor(string name, string specialty) : this()
        {
            this.DoctorName = name;
            this.Specialty = specialty;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string DoctorName
        {
            get { return this.name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name is required");
                }
                value = value.Trim();

                if (value.Length > 50)
                {
                    throw new AggregateException("Name max length = 50");
                }
                this.name = value;
            }
        }

        [Required]
        [StringLength(250)]
        public string Specialty
        {
            get { return this.specialty; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Specialty is required");
                }
                value = value.Trim();

                if (value.Length > 250)
                {
                    throw new AggregateException("Specialty max length = 250");
                }
                this.specialty = value;
            }
        }

        public virtual ICollection<Visitation> Visitations { get; set; }
    }
}
