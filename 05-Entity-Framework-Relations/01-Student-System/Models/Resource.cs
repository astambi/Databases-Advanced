using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace _01_Student_System.Models
{
    public class Resource
    {
        private string typeOfResource;
        private string url;
        public Resource()
        {
            this.Licenses = new HashSet<License>(); // Problem 4
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^(video|presentation|document|other)$", ErrorMessage = "Invalid ResourceType")]
        public string ResourceType
        {
            get { return this.typeOfResource; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Required TypeOfResource");
                }
                value = value.Trim();

                if (!Regex.IsMatch(value, @"^(video|presentation|document|other)$"))
                {
                    throw new ArgumentException("Invalid TypeOfResource. Allowed input video/presentation/document/other.");
                }

                this.typeOfResource = value;
            }
        }

        [Required]
        [RegularExpression(@"^(https?:\/\/|www\.)?(\w+\.)+[a-z]+$", ErrorMessage = "Invalid URL")]
        public string URL
        {
            get { return this.url; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Required URL");
                }
                value = value.Trim();

                if (!Regex.IsMatch(value, @"^(https?:\/\/|www\.)?(\w+\.)+[a-z]+$"))
                {
                    throw new ArgumentException("Invalid URL");
                }
                this.url = value;
            }
        }

        [Required]
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<License> Licenses { get; set; } // Problem 4
    }
}
