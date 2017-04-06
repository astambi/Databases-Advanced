namespace TeamBuilder.Models
{
    using System.Collections.Generic;

    public class Team
    {
        public Team()
        {
            this.Members = new HashSet<User>();
            this.AttendedEvents = new HashSet<Event>();
            this.Invitations = new HashSet<Invitation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Acronym { get; set; }
        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public virtual ICollection<User> Members { get; set; }
        public virtual ICollection<Event> AttendedEvents { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }
    }
}
