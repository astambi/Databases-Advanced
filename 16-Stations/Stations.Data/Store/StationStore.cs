namespace Stations.Data.Store
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class StationStore
    {
        public static void AddStations(IEnumerable<Station> stations)
        {
            using (var context = new StationsContext())
            {
                foreach (var station in stations)
                {
                    // Validate Name
                    if (station.Name == null || 
                        station.Name.Length > 50 ||
                        context.Stations.Any(s => s.Name == station.Name))
                    {
                        Console.WriteLine("Invalid data format.");
                        continue;
                    }
                    // Validate Town
                    if (station.Town != null && station.Town.Length > 50)
                    {
                        Console.WriteLine("Invalid data format.");
                        continue;
                    }
                    // Use station name if town name not provided
                    station.Town = station.Town ?? station.Name;

                    // Add station to DB
                    context.Stations.Add(station);
                    context.SaveChanges();

                    // Success Notification
                    Console.WriteLine($"Record {station.Name} successfully imported.");
                }
            }
        }
    }
}
