namespace MassDefect.Models.DTO
{
    using System.Collections.Generic;

    public class AnomalyWithVictimDto
    {
        public AnomalyWithVictimDto()
        {
            this.Victims = new HashSet<string>();
        }

        public string OriginPlanet { get; set; }
        public string TeleportPlanet { get; set; }
        public ICollection<string> Victims { get; set; }
    }
}
