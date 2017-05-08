namespace PlanetHunters.Data.Store
{
    using Models;
    using Models.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;

    public static class AstronomerStore
    {
        public static void AddAstronomers(IEnumerable<AstronomerDto> astronomers)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var astronomer in astronomers)
                {
                    // Validate Firstname
                    if (astronomer.FirstName == null || astronomer.FirstName.Length > 50)
                    {
                        Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                        continue;
                    }
                    
                    // Validate LastName
                    if (astronomer.LastName == null || astronomer.LastName.Length > 50)
                    {
                        Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                        continue;
                    }

                    // Create Astronomer Entity
                    AddEntityAstronomer(context, astronomer);

                    // Success notification
                    Console.WriteLine(string.Format(Notifications.SuccessMsg.RecordImported, astronomer.FirstName + " " + astronomer.LastName));
                }

                context.SaveChanges();
            }
        }

        private static void AddEntityAstronomer(PlanetHuntersContext context, AstronomerDto astronomer)
        {
            Astronomer astronomerEntity = new Astronomer()
            {
                FirstName = astronomer.FirstName,
                LastName = astronomer.LastName
            };
            context.Astronomers.Add(astronomerEntity);
        }

        public static List<AstronomerExportDto> GetAstronomers(string starSystemName)
        {
            using (var context = new PlanetHuntersContext())
            {
                var discoveries = context.Discoveries
                    .Where(d => d.Planets.Any(p => p.HostStarSystem.Name == starSystemName) ||
                                d.Stars.Any(s => s.HostStarSystem.Name == starSystemName));   

                List<AstronomerExportDto> astronomers = new List<AstronomerExportDto>();
                foreach (var discovery in discoveries)
                {
                    var pioneers = discovery
                        .Pioneers
                        .Select(p => new AstronomerExportDto
                        {
                            FirstName = p.FirstName,
                            LastName = p.LastName,
                            Role = "pioneer"
                        }).ToList();

                    var observers = discovery
                        .Observers
                        .Select(p => new AstronomerExportDto
                        {
                            FirstName = p.FirstName,
                            LastName = p.LastName,
                            Role = "observer"
                        }).ToList();

                    astronomers.AddRange(pioneers);
                    astronomers.AddRange(observers);
                }

                return astronomers
                        .Distinct()
                        .OrderBy(a => a.LastName)
                        .ToList();
            }
        }
    }
}
