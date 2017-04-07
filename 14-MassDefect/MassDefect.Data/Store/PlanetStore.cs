namespace MassDefect.Data.Store
{
    using Models;
    using Models.DTO;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class PlanetStore
    {
        public static void AddPlanets(IEnumerable<PlanetDto> planets)
        {
            using (MassDefectContext context = new MassDefectContext())
            {
                foreach (var planet in planets)
                {
                    // Validate input
                    if (planet.Name == null || planet.Sun == null || planet.SolarSystem == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    // Validate Solar System & Sun
                    SolarSystem solarSystem = CommandHelpers.GetSolarSystemByName(context, planet.SolarSystem);
                    Star sun = CommandHelpers.GetSunByName(context, planet.Sun);
                    if (solarSystem == null || sun == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    // Create Entity & add to DB
                    Planet planetEntity = new Planet()
                    {
                        Name = planet.Name,
                        SunId = sun.Id,
                        SolarSystemId = solarSystem.Id
                    };
                    context.Planets.Add(planetEntity);

                    // Success notification
                    Console.WriteLine($"Successfully imported Planet {planet.Name}.");
                }

                context.SaveChanges();
            }
        }

        public static List<string> GetPlanetsWithoutOriginAnomalies()
        {
            using (MassDefectContext context = new MassDefectContext())
            {
                return context.Planets
                    .Where(p => p.OriginatingAnomalies.Count == 0)
                    .Select(p => p.Name)
                    .ToList(); // NB! result from query, otherwise just query!
            }
        }
    }
}
