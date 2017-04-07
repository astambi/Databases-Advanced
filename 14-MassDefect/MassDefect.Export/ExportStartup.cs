namespace MassDefect.Export
{
    class ExportStartup
    {
        static void Main(string[] args)
        {
            /* NB! Copy EntityFrameworkSQLServer.dll 
             * from Data/bin/Debug to Export/bin/Debug
             */

            JsonExport.ExportPlanetsNotOriginatingAnomalies();
            JsonExport.ExportPeopleNotVictimsOfAnomalies();
            JsonExport.ExportAnomaliesWithMaxVictims();

            XmlExport.ExportAnomaliesAndVictims();
        }
    }
}
