namespace MassDefect.Models.DTO
{
    public class AnomalyExportDto
    {
        public int Id { get; set; }
        public string OriginPlanet { get; set; }
        public string TeleportPlanet { get; set; }
        public int VictimsCount { get; set; }
    }
}
