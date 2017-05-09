namespace Stations.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SeatingClass
    {
        public SeatingClass()
        {
            this.TrainSeats = new HashSet<TrainSeat>();
        }

        public int Id { get; set; }

        [Required, MaxLength(30)] // Unique
        public string Name { get; set; }

        [Required, MaxLength(2), MinLength(2)] // Exact length 2
        public string Abbreviation { get; set; }

        public virtual ICollection<TrainSeat> TrainSeats { get; set; }
    }
}
