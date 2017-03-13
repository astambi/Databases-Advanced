using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _05_Photographers_Albums.Models
{
    public class Photographer
    {
        public Photographer()
        {
            //this.Albums = new HashSet<Album>();           // Added Problem 6, Removed Problem 10
            this.Albums = new HashSet<PhotographerAlbum>(); // Problem 10
        }

        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; }

        // not required
        public DateTime? BirthDate { get; set; }

        //public virtual ICollection<Album> Albums { get; set; }  // Added Problem 6, Removed Problem 10

        public virtual ICollection<PhotographerAlbum> Albums { get; set; } // Problem 10
    }
}
