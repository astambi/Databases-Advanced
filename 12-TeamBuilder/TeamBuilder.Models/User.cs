namespace TeamBuilder.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public enum Gender { Male, Female }

    public class User
    {
        public User()
        {
            this.CreatedEvents = new HashSet<Event>();
            this.Teams = new HashSet<Team>();
            this.CreatedTeams = new HashSet<Team>();
            this.ReceivedInvitations = new HashSet<Invitation>();
        }

        public int Id { get; set; }

        [MinLength(3)]
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [MinLength(6)]
        public string Password { get; set; }

        public Gender Gender { get; set; }

        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<Team> CreatedTeams { get; set; }
        public virtual ICollection<Event> CreatedEvents { get; set; }
        public virtual ICollection<Invitation> ReceivedInvitations { get; set; }
    }
}
