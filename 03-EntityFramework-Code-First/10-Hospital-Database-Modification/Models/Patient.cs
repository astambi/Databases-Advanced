using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _10_Hospital_Database_Modification.Models
{
    public class Patient
    {
        private string firstName;
        private string lastName;
        private string address;
        private string email;
        private DateTime dateOfBirth;
        private bool hasMedicalHistory;

        public Patient()
        {
            this.Visitations = new HashSet<Visitation>();
            this.Diagnoses = new HashSet<Diagnose>();
            this.Medications = new HashSet<Medication>();
        }

        public Patient(string firstName, string lastName, string address, string email, DateTime dateOfBirth, bool hasMedicalHistory) :this()
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
            this.Email = email;
            this.DateOfBirth = dateOfBirth;
            this.HasMedicalInsurance = hasMedicalHistory;            
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("FirstName is required");
                }
                value = value.Trim();

                if (value.Length > 50)
                {
                    throw new AggregateException("FistName max length = 50");
                }
                this.firstName = value;
            }
        }

        [Required]
        [StringLength(50)]
        public string LastName
        {
            get { return this.lastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("LastName is required");
                }
                value = value.Trim();

                if (value.Length > 50)
                {
                    throw new AggregateException("LastName max length = 50");
                }
                this.lastName = value;
            }
        }

        [Required]
        [StringLength(250)]
        public string Address
        {
            get { return this.address; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Address is required");
                }
                value = value.Trim();

                if (value.Length > 250)
                {
                    throw new AggregateException("Address max length = 250");
                }
                this.address = value;
            }
        }

        public string Email
        {
            get { return this.email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Email is required");
                }
                value = value.Trim();

                // regex vallidation
                if (!new Regex(@"^[a-zA-Z\d]+[a-zA-Z\d._-]*[a-zA-Z\d]+@[a-zA-Z]+.[a-zA-Z]+$").IsMatch(value))
                {
                    throw new ArgumentException("Email does not match email pattern");
                }
                this.email = value;
            }
        }

        [Required]
        public DateTime DateOfBirth
        {
            get { return this.dateOfBirth; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("DateOfBirth is required");
                }
                if (value < new DateTime(1900, 1, 1))
                {
                    throw new AggregateException("DateOfBirth must be after 01/01/1900");
                }
                this.dateOfBirth = value;
            }
        }

        public int? Picture { get; set; }

        [Required]
        public bool HasMedicalInsurance
        {
            get { return this.hasMedicalHistory; }
            set
            {
                if (value != true && value != false)
                {
                    throw new ArgumentException("MedicalInsurance info is required");
                }
                this.hasMedicalHistory = value;
            }
        }

        public virtual ICollection<Visitation> Visitations { get; set; }
        public virtual ICollection<Diagnose> Diagnoses { get; set; }
        public virtual ICollection<Medication> Medications { get; set; }
    }
}