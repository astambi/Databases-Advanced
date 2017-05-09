namespace Stations.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public enum TrainType { HighSpeed, LongDistance, Freight }

    public class Train
    {
        public Train()
        {
            this.TrainSeats = new HashSet<TrainSeat>();
            this.Trips = new HashSet<Trip>();
        }

        public int Id { get; set; }

        [Required, MaxLength(10)] // Unique
        public string TrainNumber { get; set; }

        public TrainType Type { get; set; }

        public virtual ICollection<TrainSeat> TrainSeats { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
