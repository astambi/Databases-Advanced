using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_Hospital_Database.Models
{
    public class Visitation
    {
        private int patientId;
        private DateTime visitationDate;
        private string visitationComments;

        public Visitation()
        {
        }
        public Visitation(int patientId, DateTime visitationDate, string visitationComments)
        {
            this.PatientId = patientId;
            this.VisitationDate = visitationDate;
            this.VisitationComments = visitationComments;
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
        public DateTime VisitationDate
        {
            get { return this.visitationDate; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("VisitationDate is required");
                }
                if (value < new DateTime(1900, 1, 1))
                {
                    throw new AggregateException("VisitationDate must be after 01/01/1900");
                }
                this.visitationDate = value;
            }
        }

        [Required]
        [StringLength(250)]
        public string VisitationComments
        {
            get { return this.visitationComments; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("VisitationComments are required");
                }
                value = value.Trim();

                if (value.Length > 250)
                {
                    throw new AggregateException("VisitationComments max length = 250");
                }
                this.visitationComments = value;
            }
        }

        public virtual Patient Patient { get; set; }
    }
}