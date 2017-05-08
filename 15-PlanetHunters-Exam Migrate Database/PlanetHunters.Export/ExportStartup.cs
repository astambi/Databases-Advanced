namespace PlanetHunters.Export
{
    class ExportStartup
    {
        static void Main(string[] args)
        {
            // Exports saved in PlanetHunters.Export/Export

            JsonExport.ExportPlanets();
            JsonExport.ExportAstronomers();

            XmlExport.ExportStars();
        }
    }
}
