using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace _01_Student_System.Models
{
    public class Student
    {
        private string phoneNumber;

        public Student()
        {
            this.Courses = new HashSet<Course>();
            this.Homeworks = new HashSet<Homework>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // not required
        public string PhoneNumber
        {
            get { return this.phoneNumber; }
            set
            {
                if (value == null)
                {
                    this.phoneNumber = value;
                }
                value = value.Trim();

                if (!Regex.IsMatch(value, @"^\+?([ .]?\d+)+$"))
                {
                    throw new ArgumentException("Invalid PhoneNumber");
                }

                this.phoneNumber = value;
            }
        }

        [Required]
        public DateTime RegistrationDate { get; set; }

        // not required
        public DateTime? Birthday { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<Homework> Homeworks { get; set; }
    }
}
