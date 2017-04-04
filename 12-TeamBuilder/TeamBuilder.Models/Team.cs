namespace TeamBuilder.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Team
    {
        public int Id { get; set; }

        [Required, Index(IsUnique = true), MaxLength(25)]
        public string Name { get; set; } // Unique

        [MaxLength(30)]
        public string Description { get; set; }

        [Required, StringLength(3)]             // exactly 3 symbols
        public string Acronym { get; set; }

        public int CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public virtual ICollection<User> Members { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; }
    }
}
