namespace Stations.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Ticket
    {
        public int Id { get; set; }

        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }

        public decimal Price { get; set; } // non-negative

        [Required, MaxLength(8)]
        public string SeatingPlace { get; set; } // seating class abbreviation plus positive integer

        public int? PersonalCardId { get; set; } // optional
        public virtual CustomerCard PersonalCard { get; set; }
    }
}
