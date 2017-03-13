using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace _01_Student_System.Models
{
    public class Homework
    {
        private string contentType;
        
        public Homework()
        {
        }

        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [RegularExpression(@"^(application|pdf|zip)$", ErrorMessage = "Invalid ResourceType")]
        public string ContentType
        {
            get { return this.contentType; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("required ContentType");
                }
                value = value.Trim();

                if (!Regex.IsMatch(value, @"^(application|pdf|zip)$"))
                {
                    throw new ArgumentException("Invalid ContentType. Allowed input application/pdf/zip.");
                }

                this.contentType = value;
            }
        }

        [Required]
        public DateTime SubmissionDate { get; set; }

        [Required]
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        [Required]
        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
    }
}
