namespace MassDefect.Data.Store
{
    using Models;
    using Models.DTO;
    using System;
    using System.Collections.Generic;

    public static class SolarSystemStore
    {
        public static void AddSolarSystems(IEnumerable<SolarSystemDto> solarSystems)
        {
            using (MassDefectContext context = new MassDefectContext())
            {
                foreach (var solarSystem in solarSystems)
                {
                    // Validate input
                    if (solarSystem.Name == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    // Create Entity & add to DB
                    SolarSystem solarSystemEntity = new SolarSystem()
                    {
                        Name = solarSystem.Name
                    };
                    context.SolarSystems.Add(solarSystemEntity);

                    // Success notification
                    Console.WriteLine($"Successfully imported Solar System {solarSystem.Name}.");
                }

                context.SaveChanges();
            }
        }
    }
}
