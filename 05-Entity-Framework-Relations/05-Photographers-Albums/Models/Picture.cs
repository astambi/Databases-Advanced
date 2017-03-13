using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _05_Photographers_Albums.Models
{
    public class Picture // Problem 6
    {
        public Picture()
        {
            this.Albums = new HashSet<Album>();
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Caption { get; set; }

        [Required]
        public string Path { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
