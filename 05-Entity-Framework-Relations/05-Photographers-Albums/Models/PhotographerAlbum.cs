using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _05_Photographers_Albums.Models
{
    public enum Role
    {
        Owner, Viewer
    }

    public class PhotographerAlbum // Problem 10
    {
        /* use identical keys from existing mapping table PhotographerAlbums, add Role
         * add empty migration
         * edit up/ down methods => add/drop column Role in table PhotographerAlbums
         * update database */

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Photographer")]
        public int Photographer_Id { get; set; }

        public virtual Photographer Photographer { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Album")]
        public int Album_Id { get; set; }

        public virtual Album Album { get; set; }

        public Role Role { get; set; }
    }
}