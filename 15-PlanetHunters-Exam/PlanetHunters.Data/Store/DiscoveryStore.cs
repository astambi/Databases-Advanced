namespace PlanetHunters.Data.Store
{
    using Models;
    using Models.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Utilities;

    public static class DiscoveryStore
    {
        public static void AddDiscoveries(List<DiscoveryDto> discoreries)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var discovery in discoreries)
                {
                    // Validate required input: 
                    if (discovery.DateMade == null)
                    {
                        Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                        continue;
                    }

                    // Validate Date Format
                    DateTime dateMade;
                    bool isValidDateMade = DateTime.TryParseExact(discovery.DateMade, "yyyy-MM-dd",
                                            CultureInfo.InvariantCulture, DateTimeStyles.None, out dateMade);
                    if (!isValidDateMade)
                    {
                        Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                        continue;
                    }

                    // Validate Telescope
                    Telescope telescope = context.Telescopes.FirstOrDefault(t => t.Name == discovery.Telescope);
                    if (telescope == null)
                    {
                        Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                        continue;
                    }

                    bool isMissingEntity = false; // astronomer, star, planet

                    // Validate Pioneers
                    List<Astronomer> pioneers = new List<Astronomer>();
                    foreach (var pioneer in discovery.Pioneers)
                    {
                        string[] names = pioneer.Split(',').Select(x => x.Trim()).ToArray();

                        if (names.Length != 2)
                        {
                            isMissingEntity = true;
                            Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                            break;
                        }
                        string lastName = names[0];
                        string firstName = names[1];

                        if (firstName == null || lastName == null)
                        {
                            isMissingEntity = true;
                            Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                            break;
                        }
                        Astronomer astronomer = context.Astronomers.FirstOrDefault(a => a.FirstName == firstName &&
                                                                                        a.LastName == lastName);
                        if (astronomer == null)
                        {
                            isMissingEntity = true;
                            Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                            break;
                        }

                        pioneers.Add(astronomer);
                    }

                    // Validate Observers
                    List<Astronomer> observers = new List<Astronomer>();
                    foreach (var observer in discovery.Observers)
                    {
                        string[] names = observer.Split(',').Select(x => x.Trim()).ToArray();

                        if (names.Length != 2)
                        {
                            isMissingEntity = true;
                            Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                            break;
                        }
                        string lastName = names[0];
                        string firstName = names[1];

                        if (firstName == null || lastName == null)
                        {
                            isMissingEntity = true;
                            Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                            break;
                        }
                        Astronomer astronomer = context.Astronomers.FirstOrDefault(a => a.FirstName == firstName &&
                                                                                        a.LastName == lastName);
                        if (astronomer == null)
                        {
                            isMissingEntity = true;
                            Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                            break;
                        }

                        observers.Add(astronomer);
                    }

                    // Validate Stars
                    List<Star> stars = new List<Star>();
                    foreach (var starName in discovery.Stars)
                    {
                        if (starName == null)
                        {
                            isMissingEntity = true;
                            Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                            break;
                        }

                        Star star = context.Stars.FirstOrDefault(s => s.Name == starName);
                        if (star == null)
                        {
                            isMissingEntity = true;
                            Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                            break;
                        }

                        stars.Add(star);
                    }

                    // Validate Planets
                    List<Planet> planets = new List<Planet>();
                    foreach (var planetName in discovery.Planets)
                    {
                        if (planetName == null)
                        {
                            isMissingEntity = true;
                            Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                            break;
                        }

                        Planet planet = context.Planets.FirstOrDefault(p => p.Name == planetName);
                        if (planet == null)
                        {
                            isMissingEntity = true;
                            Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                            break;
                        }

                        planets.Add(planet);
                    }

                    // Validate non-existing Astronomers, Stars & Planets 
                    if (isMissingEntity)
                    {
                        Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                        continue;
                    }

                    // Create Discovery Entity
                    AddEntityDiscovery(context, dateMade, telescope, pioneers, observers, stars, planets);

                    // Success
                    Console.WriteLine($"Discovery ({dateMade:yyyy/MM/dd}-{telescope.Name}) with {stars.Count} star(s), {planets.Count} planet(s), {pioneers.Count} pioneer(s) and {observers.Count} observers successfully  imported.");
                }

                context.SaveChanges();
            }
        }

        private static void AddEntityDiscovery(PlanetHuntersContext context, DateTime dateMade, Telescope telescope, List<Astronomer> pioneers, List<Astronomer> observers, List<Star> stars, List<Planet> planets)
        {
            Discovery discoveryEntity = new Discovery()
            {
                DateMade = new DateTime(dateMade.Year, dateMade.Month, dateMade.Day),
                TelescopeId = telescope.Id,
                Pioneers = pioneers,
                Stars = stars,
                Planets = planets,
                Observers = observers
            };
            context.Discoveries.Add(discoveryEntity);
        }
    }
}
