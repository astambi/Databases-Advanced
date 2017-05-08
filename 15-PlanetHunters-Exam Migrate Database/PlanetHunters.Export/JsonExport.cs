namespace PlanetHunters.Export
{
    using Data.Store;
    using Models.DTOs;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class JsonExport
    {
        public static void ExportPlanets()
        {
            Console.Write("Enter telescope (e.g. TRAPPIST): ");
            string telescopeName = Console.ReadLine();

            IEnumerable<PlanetExportDto> planets = PlanetStore.GetPlanets(telescopeName);

            string json = JsonConvert.SerializeObject(planets
                .Select(p => new
                {
                    Name = p.Name,
                    Mass = p.Mass,
                    Orbitting = new[] { p.Orbitting }
                }),
                Formatting.Indented);

            File.WriteAllText($"../../Export/planets-by-{telescopeName}.json", json);
        }

        public static void ExportAstronomers()
        {
            Console.Write("Enter star system (e.g. Alpha Centauri): ");
            string starSystemName = Console.ReadLine();

            List<AstronomerExportDto> astronomers = AstronomerStore.GetAstronomers(starSystemName);

            string json = JsonConvert.SerializeObject(astronomers
                .Select(a => new
                {
                    Name = a.FirstName + " " + a.LastName,
                    Role = a.Role
                }), Formatting.Indented);

            File.WriteAllText($"../../Export/astronomers-of-{starSystemName}.json", json);
        }
    }
}
