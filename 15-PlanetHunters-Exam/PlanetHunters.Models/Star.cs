namespace PlanetHunters.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Star
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public int Temperature { get; set; }

        [Required]
        public int HostStarSystemId { get; set; }
        public virtual StarSystem HostStarSystem { get; set; }

    }
}
