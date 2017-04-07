namespace MassDefect.Import
{
    using Data.Store;
    using Models.DTO;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;

    public static class JsonImport
    {
        public static void ImportSolarSystems()
        {
            string json = File.ReadAllText("../../../datasets/solar-systems.json");
            var systems = JsonConvert.DeserializeObject<IEnumerable<SolarSystemDto>>(json);
            SolarSystemStore.AddSolarSystems(systems);
        }

        public static void ImportStars()
        {
            string json = File.ReadAllText("../../../datasets/stars.json");
            var stars = JsonConvert.DeserializeObject<IEnumerable<StarDto>>(json);
            StarStore.AddStars(stars);
        }

        public static void ImportPlanets()
        {
            string json = File.ReadAllText("../../../datasets/planets.json");
            var planets = JsonConvert.DeserializeObject<IEnumerable<PlanetDto>>(json);
            PlanetStore.AddPlanets(planets);
        }

        public static void ImportPersons()
        {
            string json = File.ReadAllText("../../../datasets/persons.json");
            var persons = JsonConvert.DeserializeObject<IEnumerable<PersonDto>>(json);
            PersonStore.AddPersons(persons);
        }

        public static void ImportAnomalies()
        {
            string json = File.ReadAllText("../../../datasets/anomalies.json");
            var anomalies = JsonConvert.DeserializeObject<IEnumerable<AnomalyDto>>(json);
            AnomalyStore.AddAnomalies(anomalies);
        }

        public static void ImportAnomalyVictims()
        {
            string json = File.ReadAllText("../../../datasets/anomaly-victims.json");
            var anomalyVictims = JsonConvert.DeserializeObject<IEnumerable<AnomalyVictimDto>>(json);
            AnomalyStore.AddAnomalyVictims(anomalyVictims);
        }
    }
}
