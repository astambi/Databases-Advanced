namespace Stations.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public enum CardType { Pupil, Student, Elder, Debilitated, Normal }

    public class CustomerCard
    {
        public CustomerCard()
        {
            this.CardType = CardType.Normal;
            this.Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }

        [Required, MaxLength(128)]
        public string Name { get; set; }

        [Range(0, 120)]
        public int Age { get; set; } // [0, 120]

        public CardType CardType { get; set; } // default value Normal

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
