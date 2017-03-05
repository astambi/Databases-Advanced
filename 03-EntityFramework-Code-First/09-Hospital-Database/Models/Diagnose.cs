using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_Hospital_Database.Models
{
    public class Diagnose
    {
        private int patientId;
        private string diagnoseName;
        private string diagnoseComments;

        public Diagnose()
        {
        }
        public Diagnose(int patientId, string diagnoseName, string diagnoseComments)
        {
            this.PatientId = patientId;
            this.DiagnoseName = diagnoseName;
            this.DiagnoseComments = diagnoseComments;
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
        public string DiagnoseName
        {
            get { return this.diagnoseName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("DiagnoseName is required");
                }
                value = value.Trim();

                if (value.Length > 50)
                {
                    throw new AggregateException("DiagnoseName max length = 50");
                }
                this.diagnoseName = value;
            }
        }

        [Required]
        [StringLength(250)]
        public string DiagnoseComments
        {
            get { return this.diagnoseComments; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("DiagnoseComments are required");
                }
                value = value.Trim();

                if (value.Length > 250)
                {
                    throw new AggregateException("DiagnoseComments max length = 250");
                }
                this.diagnoseComments = value;
            }
        }

        public virtual Patient Patient { get; set; }
    }
}