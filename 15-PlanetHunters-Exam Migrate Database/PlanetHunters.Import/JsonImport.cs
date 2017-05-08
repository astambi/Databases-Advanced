namespace PlanetHunters.Import
{
    using Data.Store;
    using Models.DTOs;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;

    public static class JsonImport
    {
        public static void ImportAstronomers()
        {
            string json = ReadJsonFile("astronomers");
            var astronomers = JsonConvert.DeserializeObject<IEnumerable<AstronomerDto>>(json);
            AstronomerStore.AddAstronomers(astronomers);
        }

        public static void ImportTelescopes()
        {
            string json = ReadJsonFile("telescopes");
            var telescopes = JsonConvert.DeserializeObject<IEnumerable<TelescopeDto>>(json);
            TelescopeStore.AddTelescopes(telescopes);
        }

        public static void ImportPlanets()
        {
            string json = ReadJsonFile("planets");
            var planets = JsonConvert.DeserializeObject<IEnumerable<PlanetDto>>(json);
            PlanetStore.AddPlanets(planets);
        }
        
        private static string ReadJsonFile(string fileName)
        {
            return File.ReadAllText($"../../../Import/{fileName}.json");
        }
    }
}
