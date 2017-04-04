namespace TeamBuilder.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public enum Gender { Male, Female }

    public class User
    {
        public User()
        {
            this.CreatedEvents = new List<Event>();             // not HashSet???
            this.Teams = new List<Team>();
            this.CreatedTeams = new List<Team>();
            this.ReceivedInvitations = new List<Invitation>();
        }

        public int Id { get; set; }

        [Required, MinLength(3), MaxLength(25)]
        public string Username { get; set; }

        [MaxLength(25)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        [Required, MinLength(6), MaxLength(30)]
        public string Password { get; set; }

        public Gender Gender { get; set; }

        public int Age { get; set; } // should age not be nullable ??

        public bool IsDeleted { get; set; }

        //[InverseProperty("Creator")]
        public virtual ICollection<Event> CreatedEvents { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<Team> CreatedTeams { get; set; }

        //[InverseProperty("InvitedUser")]
        public virtual ICollection<Invitation> ReceivedInvitations { get; set; }
    }
}
