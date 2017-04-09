namespace PlanetHunters.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Astronomer
    {
        public Astronomer()
        {
            this.DiscoveriesMade = new HashSet<Discovery>();
            this.DiscoveryObservations = new HashSet<Discovery>();
        }

        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        public virtual ICollection<Discovery> DiscoveriesMade { get; set; }
        public virtual ICollection<Discovery> DiscoveryObservations { get; set; }
    }
}
