namespace Stations.Data.Store
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SeatingClassStore
    {
        public static void AddSeatingClasses(IEnumerable<SeatingClass> seatingClasses)
        {
            using (var context = new StationsContext())
            {
                foreach (var seatingClass in seatingClasses)
                {
                    // Validate Name
                    if (seatingClass.Name == null || seatingClass.Name.Length > 30 ||
                        context.SeatingClasses.Any(c => c.Name == seatingClass.Name))
                    {
                        Console.WriteLine("Invalid data format.");
                        continue;
                    }
                    // Validate Abbreviation
                    if (seatingClass.Abbreviation == null || seatingClass.Abbreviation.Length != 2)
                    {
                        Console.WriteLine("Invalid data format.");
                        continue;
                    }
                    
                    // Add seating class to DB
                    context.SeatingClasses.Add(seatingClass);
                    context.SaveChanges();

                    // Success Notification
                    Console.WriteLine($"Record {seatingClass.Name} successfully imported.");
                }
            }
        }
    }
}
