namespace MassDefect.Import
{
    public class ImportStartup
    {
        static void Main(string[] args)
        {
            /* NB! Copy EntityFrameworkSQLServer.dll 
             * from Data/bin/Debug to Import/bin/Debug
             */

            JsonImport.ImportSolarSystems();
            JsonImport.ImportStars();
            JsonImport.ImportPlanets();
            JsonImport.ImportPersons();
            JsonImport.ImportAnomalies();
            JsonImport.ImportAnomalyVictims();

            XmlImport.ImportAnomalyVictims();
        }
    }
}
