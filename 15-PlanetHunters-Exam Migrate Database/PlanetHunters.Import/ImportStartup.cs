namespace PlanetHunters.Import
{
    class ImportStartup
    {
        static void Main(string[] args)
        {
            // Import files saved in Solution/Import

            JsonImport.ImportAstronomers();
            JsonImport.ImportTelescopes();
            JsonImport.ImportPlanets();

            XmlImport.ImportStars();
            XmlImport.ImportDiscoveries();
        }
    }
}
