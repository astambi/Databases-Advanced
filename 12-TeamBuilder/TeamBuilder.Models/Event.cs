namespace TeamBuilder.Models
{
    using System;
    using System.Collections.Generic;

    public class Event
    {
        public Event()
        {
            this.ParticipatingTeams = new HashSet<Team>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public virtual ICollection<Team> ParticipatingTeams { get; set; }
    }
}
