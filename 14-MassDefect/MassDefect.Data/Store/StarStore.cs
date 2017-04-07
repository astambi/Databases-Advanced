namespace MassDefect.Data.Store
{
    using Models;
    using Models.DTO;
    using System;
    using System.Collections.Generic;

    public static class StarStore
    {
        public static void AddStars(IEnumerable<StarDto> stars)
        {
            using (MassDefectContext context = new MassDefectContext())
            {
                foreach (var star in stars)
                {
                    // Validate input
                    if (star.Name == null || star.SolarSystem == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    // Validate Solar System
                    SolarSystem solarSystem = CommandHelpers.GetSolarSystemByName(context, star.SolarSystem);
                    if (solarSystem == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    // Create Entity & add to DB
                    Star starEntity = new Star()
                    {
                        Name = star.Name,
                        SolarSystemId = solarSystem.Id
                    };
                    context.Stars.Add(starEntity);

                    // Success notification
                    Console.WriteLine($"Successfully imported Star {star.Name}.");
                }

                context.SaveChanges();
            }
        }
    }
}
