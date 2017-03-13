﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _01_Student_System.Models
{
    public class Course
    {
        public Course()
        {
            this.Students = new HashSet<Student>();
            this.Resources = new HashSet<Resource>();
            this.HomeworkSubmissions = new HashSet<Homework>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // not required
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }

        public virtual ICollection<Homework> HomeworkSubmissions { get; set; }
    }
}
