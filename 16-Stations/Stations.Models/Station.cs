namespace Stations.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Station
    {
        public Station()
        {
            this.OriginatingTrips = new HashSet<Trip>();
            this.DestinationTrips = new HashSet<Trip>();
        }

        public int Id { get; set; }

        [Required, MaxLength(50)] // Unique
        public string Name { get; set; }

        [MaxLength(50)]
        public string Town { get; set; }

        public virtual ICollection<Trip> OriginatingTrips { get; set; }
        public virtual ICollection<Trip> DestinationTrips { get; set; }
    }
}
