using _05_Photographers_Albums.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace _05_Photographers_Albums.Models
{
    public class Tag // Problem 7
    {
        private string name;

        public Tag()
        {
            this.Albums = new HashSet<Album>();
        }

        [Key]
        [Index(IsUnique = true)]
        //[Tag] // Problem 8 - Uncomment to test Attribute Validation
        public string Name
        {
            get { return this.name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Required TagName");
                }
                value = value.Trim();

                // Problem 7 - throw exception for invalid tags => Disable for Problem 8
                //if (!Regex.IsMatch(value, @"^#\S+$"))
                //{
                //    throw new ArgumentException("Invalid TagName");
                //}

                // Problem 8 - transform invalid tags into valid ones => Comment to test Attribute Validation
                if (!Regex.IsMatch(value, @"^#\S{1,19}$"))
                {
                    value = TagTransformer.Transform(value);
                }
                // Problem 8 - transform invalid tags into valid ones => Comment to test Attribute Validation

                this.name = value;
            }
        }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
