namespace MassDefect.Models.DTO
{
    using System.Collections.Generic;

    public class AnomaliesWithVictimsExportDto
    {
        public AnomaliesWithVictimsExportDto()
        {
            this.Victims = new HashSet<string>();
        }

        public int Id { get; set; }
        public string OriginPlanet { get; set; }
        public string TeleportPlanet { get; set; }
        public ICollection<string> Victims { get; set; }
    }
}
