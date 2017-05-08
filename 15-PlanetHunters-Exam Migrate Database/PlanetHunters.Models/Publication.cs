namespace PlanetHunters.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Publication // Bonus Task: Migrate Database
    {
        public int Id { get; set; }

        [Required, Column(TypeName = "date")] // NB! for 1689-12-01
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int DiscoveryId { get; set; }
        public virtual Discovery Discovery { get; set; }

        public int JournalId { get; set; }
        public virtual Journal Journal { get; set; }
    }
}
