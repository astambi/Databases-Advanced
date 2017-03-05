using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07_Gringotts_Database.Models
{
    public class WizardDeposit
    {
        private string firstName = null;
        private string lastName;
        private string notes = null;
        private int age;
        private string magicWandCreator = null;
        private short? magicWandSize;
        private string depositGroup = null;

        public WizardDeposit()
        {
        }
        public WizardDeposit(string lastName, int age)
        {
            this.LastName = lastName;
            this.Age = age;
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                if (value != null && value.Trim().Length > 50)
                {
                    throw new ArgumentException("FirstName max length = 50");
                }
                this.firstName = value.Trim();
            }
        }

        [Required]
        [StringLength(60)]
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

                if (value.Length > 60)
                {
                    throw new ArgumentException("LastName max length = 60");
                }
                this.lastName = value;
            }
        }

        [StringLength(1000)]
        public string Notes
        {
            get { return this.notes; }
            set
            {
                if (value!= null && value.Trim().Length > 1000)
                {
                    throw new ArgumentException("Notes max length = 60");
                }
                this.notes = value.Trim();
            }
        }

        [Required]
        public int Age
        {
            get { return this.age; }
            set
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    throw new ArgumentException("Age is required");
                }
                if (value < 0)
                {
                    throw new ArgumentException("Age cannot be negative");
                }
                this.age = value;
            }
        }

        [StringLength(100)]
        public string MagicWandCreator
        {
            get { return this.magicWandCreator; }
            set
            {
                if (value!= null && value.Trim().Length > 100)
                {
                    throw new ArgumentException("MagicWandCreator max length = 100");
                }
                this.magicWandCreator = value.Trim();
            }
        }

        public short? MagicWandSize
        {
            get { return this.magicWandSize; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("MagicWandSize min value = 1");
                }
                this.magicWandSize = value;
            }
        }

        [StringLength(20)]
        public string DepositGroup
        {
            get { return this.depositGroup; }
            set
            {
                if (value!= null && value.Trim().Length > 20)
                {
                    throw new ArgumentException("DepositGroup max length = 20");
                }
                this.depositGroup = value.Trim();
            }
        }

        public DateTime? DepositStartDate { get; set; }

        public decimal? DepositAmount { get; set; }

        public decimal? DepositInterest { get; set; }

        public double? DepositCharge { get; set; } // see example

        public DateTime? DepositExpirationDate { get; set; }

        public bool? IsDepositExpired { get; set; }
    }
}