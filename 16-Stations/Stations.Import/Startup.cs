namespace Stations.Import
{
    using Data;

    class Startup
    {
        static void Main(string[] args)
        {
            //Init.InitializeDatabase();

            JsonImport.ImportStations();
            JsonImport.ImportSeatingClasses();
            JsonImport.ImportTrains();
            JsonImport.ImportTrips();
            XmlImport.ImportPersonCards();
            XmlImport.ImportTickets();
        }
    }
}
