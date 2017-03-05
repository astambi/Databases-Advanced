using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10_Hospital_Database_Modification.Models
{
    public class Medication
    {
        private int patientId;
        private string medicationName;

        public Medication()
        {
        }
        public Medication(int patientId, string medicationName)
        {
            this.PatientId = patientId;
            this.MedicationName = medicationName;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int PatientId
        {
            get { return this.patientId; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Invalid PatientId");
                }
                this.patientId = value;
            }
        }

        [Required]
        [StringLength(50)]
        public string MedicationName
        {
            get { return this.medicationName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("MedicationName is required");
                }
                value = value.Trim();

                if (value.Length > 50)
                {
                    throw new AggregateException("MedicationName max length = 50");
                }
                this.medicationName = value;
            }
        }

        public virtual Patient Patient { get; set; }
    }
}
