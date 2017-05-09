namespace Stations.Import
{
    using Data.Store;
    using Models;
    using Models.DTOs;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;

    public static class JsonImport
    {
        public static void ImportStations()
        {
            string json = ReadJsonFile("stations");
            var stations = JsonConvert.DeserializeObject<IEnumerable<Station>>(json);
            StationStore.AddStations(stations);
        }
        
        internal static void ImportSeatingClasses()
        {
            string json = ReadJsonFile("classes");
            var seatingClasses = JsonConvert.DeserializeObject<IEnumerable<SeatingClass>>(json);
            SeatingClassStore.AddSeatingClasses(seatingClasses);
        }

        internal static void ImportTrains()
        {
            string json = ReadJsonFile("trains");
            var trainDtos = JsonConvert.DeserializeObject<IEnumerable<TrainImportDto>>(json);
            TrainStore.ImportTrains(trainDtos);
        }

        internal static void ImportTrips()
        {
            string json = ReadJsonFile("trips");
            var tripDtos = JsonConvert.DeserializeObject<IEnumerable<TripImportDto>>(json);
            TripStore.AddTrips(tripDtos);
        }

        private static string ReadJsonFile(string fileName)
        {
            return File.ReadAllText($"../../Import/{fileName}.json");
        }
    }
}
