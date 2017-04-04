namespace TeamBuilder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Event
    {
        public Event()
        {
            this.ParticipatingTeams = new List<Team>();
        }

        public int Id { get; set; }

        [Required, MaxLength(25)]               // Unicode
        public string Name { get; set; }

        [MaxLength(250)]                        // Unicode
        public string Description { get; set; }

        public DateTime StartDate { get; set; } // format

        public DateTime EndDate { get; set; }   // format, after StartDate

        public int CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public virtual ICollection<Team> ParticipatingTeams { get; set; }
    }
}
