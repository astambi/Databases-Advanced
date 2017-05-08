namespace PlanetHunters.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Discovery
    {
        public Discovery()
        {
            this.Stars = new HashSet<Star>();
            this.Planets = new HashSet<Planet>();
            this.Pioneers = new HashSet<Astronomer>();
            this.Observers = new HashSet<Astronomer>();
        }

        public int Id { get; set; }

        [Required, Column(TypeName="date")] // NB! for 1689-12-01
        public DateTime DateMade { get; set; }

        [Required]
        public int TelescopeId { get; set; }
        public virtual Telescope Telescope { get; set; }

        public virtual ICollection<Star> Stars { get; set; }
        public virtual ICollection<Planet> Planets { get; set; }
        public virtual ICollection<Astronomer> Pioneers { get; set; }
        public virtual ICollection<Astronomer> Observers { get; set; }
    }
}
