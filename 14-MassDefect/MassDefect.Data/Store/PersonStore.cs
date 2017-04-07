namespace MassDefect.Data.Store
{
    using Models;
    using Models.DTO;
    using System;
    using System.Collections.Generic;

    public static class PersonStore
    {
        public static void AddPersons(IEnumerable<PersonDto> persons)
        {
            using (MassDefectContext context = new MassDefectContext())
            {
                foreach (var person in persons)
                {
                    // Validate input
                    if (person.Name == null || person.HomePlanet == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    // Validate HomePlanet
                    Planet homePlanet = CommandHelpers.GetPlanetByName(context, person.HomePlanet);
                    if (homePlanet == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    // Create Entity & add to DB
                    Person personEntity = new Person()
                    {
                        Name = person.Name,
                        HomePlanetId = homePlanet.Id
                    };
                    context.Persons.Add(personEntity);

                    // Success notification
                    Console.WriteLine($"Successfully imported Person {person.Name}.");
                }

                context.SaveChanges();
            }
        }
    }
}
