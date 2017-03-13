using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _05_Photographers_Albums.Models
{
    public class Album // Problem 6
    {
        public Album()
        {
            this.Pictures = new HashSet<Picture>();
            this.Tags = new HashSet<Tag>();                         // Problem 7
            //this.Photographers = new HashSet<Photographer>();     // Added Problem 9, Removed Problem 10
            this.Photographers = new HashSet<PhotographerAlbum>();  // Problem 10
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string BackgroundColor { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        //[Required]                                             // Problem 9
        //public int PhotographerId { get; set; }                // Problem 9
        //public virtual Photographer Photographer { get; set; } // Problem 9

        //public virtual ICollection<Photographer> Photographers { get; set; } // Added Problem 9, Removed Problem 10

        public virtual ICollection<PhotographerAlbum> Photographers { get; set; } // Problem 10

        public virtual ICollection<Picture> Pictures { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }      // Problem 7
    }
}
