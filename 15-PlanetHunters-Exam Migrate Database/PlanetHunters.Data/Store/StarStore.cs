namespace PlanetHunters.Data.Store
{
    using Models;
    using Models.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;

    public static class StarStore
    {
        public static void AddStars(List<StarDto> starsDto)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var star in starsDto)
                {
                    // Validate required input
                    if (star.Name == null || star.Temperature == null || star.StarSystem == null)
                    {
                        Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                        continue;
                    }

                    // Validate Name
                    if (star.Name.Length > 255)
                    {
                        Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                        continue;
                    }

                    // Validate Mass
                    int temperature;
                    bool isNumberTemp = int.TryParse(star.Temperature, out temperature);
                    if (!isNumberTemp)
                    {
                        Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                        continue;
                    }
                    if (temperature <= 0)
                    {
                        Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                        continue;
                    }

                    // Validate StarSystem
                    StarSystem starSystem = context.StarSystems.FirstOrDefault(ss => ss.Name == star.StarSystem);

                    bool isNewStarSystem = false;
                    if (starSystem == null)
                    {
                        isNewStarSystem = true;
                        starSystem = AddEntityStarSystem(context, star.StarSystem); // Add new Star System
                    }

                    // Create Star Entity
                    AddEntityStar(context, star.Name, temperature, starSystem.Id);

                    // Success notification
                    Console.WriteLine(string.Format(Notifications.SuccessMsg.RecordImported, star.Name));

                    if (isNewStarSystem)
                    {
                        Console.WriteLine(string.Format(Notifications.SuccessMsg.RecordImported, star.StarSystem));
                    }
                }

                context.SaveChanges();
            }
        }

        private static void AddEntityStar(PlanetHuntersContext context, string starName, int temperature, int starSystemId)
        {
            Star starEntity = new Star()
            {
                Name = starName,
                Temperature = temperature,
                HostStarSystemId = starSystemId
            };
            context.Stars.Add(starEntity);
        }

        private static StarSystem AddEntityStarSystem(PlanetHuntersContext context, string starSystemName)
        {
            StarSystem starSystem = new StarSystem() { Name = starSystemName };
            context.StarSystems.Add(starSystem);
            context.SaveChanges();

            return starSystem;
        }

        public static List<StarExportDto> GetStars()
        {
            using (var context = new PlanetHuntersContext())
            {
                var discoveries = context.Discoveries.Where(d => d.Stars.Count > 0);

                var starsExportDto = new List<StarExportDto>();

                foreach (var discovery in discoveries)
                {
                    // Stars
                    var stars = discovery.Stars
                       .Select(s => new StarDto
                       {
                           Name = s.Name,
                           Temperature = s.Temperature.ToString(),
                           StarSystem = s.HostStarSystem.Name
                       }).ToList();

                    // Astronomers
                    var observers = discovery.Observers
                        .Select(a => new AstronomerExportDto
                        {
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            Role = "false"
                        }).ToList();
                    var pioneers = discovery.Pioneers
                        .Select(a => new AstronomerExportDto
                        {
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            Role = "true"
                        }).ToList();
                    var astronomers = new List<AstronomerExportDto>();
                    astronomers.AddRange(pioneers);
                    astronomers.AddRange(observers);

                    astronomers = astronomers
                        .OrderBy(a => a.FirstName + " " + a.LastName)
                        .ToList();

                    // Export DTO
                    var starExportDto = new StarExportDto
                    {
                        DiscoveryDate = discovery.DateMade,
                        Telescope = discovery.Telescope.Name,
                        Stars = stars,
                        Astronomers = astronomers
                    };

                    starsExportDto.Add(starExportDto);
                }
                return starsExportDto;
            }
        }
    }
}
