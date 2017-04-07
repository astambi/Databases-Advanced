namespace MassDefect.Export
{
    using Data.Store;
    using Models.DTO;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class JsonExport
    {
        public static void ExportPlanetsNotOriginatingAnomalies()
        {
            List<string> planets = PlanetStore.GetPlanetsWithoutOriginAnomalies();

            string json = JsonConvert.SerializeObject(
                planets.Select(p => new { Name = p }),
                Formatting.Indented);

            File.WriteAllText("../../Export/planets.json", json);
        }

        internal static void ExportPeopleNotVictimsOfAnomalies()
        {
            List<PersonDto> people = PersonStore.GetPeopleNotVictimsOfAnomalies();
            string json = JsonConvert.SerializeObject(
                people.Select(p => new
                {
                    Name = p.Name,
                    HomePlanet = new { Name = p.HomePlanet }
                }),
                Formatting.Indented);

            File.WriteAllText("../../Export/people.json", json);
        }

        internal static void ExportAnomaliesWithMaxVictims()
        {
            List<AnomalyExportDto> anomaly = AnomalyStore.GetAnomalyWithMaxVictims();
            string json = JsonConvert.SerializeObject(
                anomaly.Select(a => new
                {
                    Id = a.Id,
                    OriginPlanet = new { Name = a.OriginPlanet },
                    TeleportPlanet = new { Name = a.TeleportPlanet },
                    VictimsCount = a.VictimsCount
                }),
                Formatting.Indented);

            File.WriteAllText("../../Export/anomaly.json", json);
        }
    }
}
