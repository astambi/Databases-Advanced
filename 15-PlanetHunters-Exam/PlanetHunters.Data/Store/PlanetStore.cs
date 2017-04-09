namespace PlanetHunters.Data.Store
{
    using Models;
    using Models.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;

    public static class PlanetStore
    {
        public static void AddPlanets(IEnumerable<PlanetDto> planets)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var planet in planets)
                {
                    // Validate required input
                    if (planet.Name == null || planet.Mass.ToString() == null || planet.StarSystem == null)
                    {
                        Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                        continue;
                    }

                    // Validate Name
                    if (planet.Name.Length > 255)
                    {
                        Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                        continue;
                    }

                    // Validate Mass
                    if (planet.Mass <= 0)
                    {
                        Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                        continue;
                    }

                    // Validate Star System
                    StarSystem starSystem = context.StarSystems.FirstOrDefault(s => s.Name == planet.StarSystem);
                    bool isNewStarSystem = false;

                    if (starSystem == null)
                    {
                        isNewStarSystem = true;
                        starSystem = AddStarEntity(context, planet.StarSystem); // Create Star Entity
                    }

                    // Create Planet Entity
                    AddPlanetEntity(context, planet.Name, planet.Mass, starSystem.Id);

                    // Success notification Planet & StarSystem
                    Console.WriteLine(string.Format(Notifications.SuccessMsg.RecordImported, planet.Name));

                    if (isNewStarSystem)
                    {
                        Console.WriteLine(string.Format(Notifications.SuccessMsg.RecordImported, planet.StarSystem));
                    }
                }

                context.SaveChanges();
            }
        }

        private static StarSystem AddStarEntity(PlanetHuntersContext context, string starSystemName)
        {
            StarSystem starSystem = new StarSystem() { Name = starSystemName };
            context.StarSystems.Add(starSystem);
            context.SaveChanges();

            return starSystem;
        }

        private static void AddPlanetEntity(PlanetHuntersContext context, string planetName, float planetMass, int starSystemId)
        {
            Planet planetEntity = new Planet()
            {
                Name = planetName,
                Mass = planetMass,
                HostStarSystemId = starSystemId
            };
            context.Planets.Add(planetEntity);
        }

        public static IEnumerable<PlanetExportDto> GetPlanets(string telescopeName)
        {
            using (var context = new PlanetHuntersContext())
            {
                //var planets = context.Discoveries
                //    .Where(d => d.Telescope.Name == telescopeName)
                //    .Select(d => d.Planets.Select(p => new PlanetExportDto
                //    {
                //        Name = p.Name,
                //        Mass = p.Mass,
                //        Orbitting = p.HostStarSystem.Name
                //    }))
                //    .ToList();

                //return planets[1];

                var discoveries = context.Discoveries
                    .Where(d => d.Telescope.Name == telescopeName);

                List<PlanetExportDto> planets = new List<PlanetExportDto>();
                foreach (var discovery in discoveries)
                {
                    var planetsDto = discovery
                        .Planets
                        .Select(p => new PlanetExportDto
                        {
                            Name = p.Name,
                            Mass = p.Mass,
                            Orbitting = p.HostStarSystem.Name
                        })
                        .ToList();

                    planets.AddRange(planetsDto);
                }

                return planets
                        .OrderByDescending(p => p.Mass)
                        .ToList();
            }
        }
    }
}
