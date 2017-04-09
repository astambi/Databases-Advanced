namespace PlanetHunters.Import
{
    class ImportStartup
    {
        static void Main(string[] args)
        {
            // Import files save in Solution/Import

            JsonImport.ImportAstronomers();
            JsonImport.ImportTelescopers();
            JsonImport.ImportPlanets();

            XmlImport.ImportStars();
            XmlImport.ImportDiscoveries();
        }
    }
}
