namespace MassDefect.Data.Store
{
    using Models;
    using Models.DTO;
    using System;
    using System.Collections.Generic;

    public static class AnomalyStore
    {
        public static void AddAnomalies(IEnumerable<AnomalyDto> anomalies)
        {
            using (MassDefectContext context = new MassDefectContext())
            {
                foreach (var anomaly in anomalies)
                {
                    // Validate input
                    if (anomaly.OriginPlanet == null || anomaly.TeleportPlanet == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    // Validate Planets
                    Planet originPlanet = CommandHelpers.GetPlanetByName(context, anomaly.OriginPlanet);
                    Planet teleportPlanet = CommandHelpers.GetPlanetByName(context, anomaly.TeleportPlanet);

                    if (originPlanet == null || teleportPlanet == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    // Create Entity & add to DB
                    Anomaly anomalyEntity = new Anomaly()
                    {
                        OriginPlanetId = originPlanet.Id,
                        TeleportPlanetId = teleportPlanet.Id
                    };
                    context.Anomalies.Add(anomalyEntity);

                    // Success notification
                    Console.WriteLine($"Successfully imported anomaly.");
                }

                context.SaveChanges();
            }
        }

        public static void AddAnomalyVictims(IEnumerable<AnomalyVictimDto> anomalyVictims)
        {
            using (MassDefectContext context = new MassDefectContext())
            {
                foreach (var anomalyVictim in anomalyVictims)
                {
                    // Validate input
                    if (anomalyVictim.Id <= 0 || anomalyVictim.Person == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    // Validate Id & Person
                    Anomaly anomaly = CommandHelpers.GetAnomalyById(context, anomalyVictim.Id);
                    Person person = CommandHelpers.GetPersonByName(context, anomalyVictim.Person);

                    if (anomaly == null || person == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    // Add victims to anomalies & add to DB
                    anomaly.Victims.Add(person);

                    // No success notification
                }

                context.SaveChanges();
            }
        }

        public static void AddNewAnomaliesWithVictims(IEnumerable<AnomalyWithVictimDto> anomaliesWithVictims)
        {
            using (MassDefectContext context = new MassDefectContext())
            {
                foreach (var anomaly in anomaliesWithVictims)
                {
                    // Validate input
                    if (anomaly.OriginPlanet == null || anomaly.TeleportPlanet == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    // Validate Planets
                    Planet originPlanet = CommandHelpers.GetPlanetByName(context, anomaly.OriginPlanet);
                    Planet teleportPlanet = CommandHelpers.GetPlanetByName(context, anomaly.TeleportPlanet);
                    if (originPlanet == null || teleportPlanet == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    // Validate Anomaly
                    Anomaly anomalyEntity = CommandHelpers.GetAnomalyByPlanetsId(context, originPlanet.Id, teleportPlanet.Id);
                    
                    // Create new anomaly if non-existent
                    if (anomalyEntity == null)  
                    {
                        anomalyEntity = new Anomaly()
                        {
                            OriginPlanetId = originPlanet.Id,
                            TeleportPlanetId = teleportPlanet.Id
                        };
                        context.Anomalies.Add(anomalyEntity);
                    }

                    // Add victims to anomaly
                    foreach (var victim in anomaly.Victims)
                    {
                        // Validate Victim input
                        if (victim == null)
                        {
                            Console.WriteLine("Error: Invalid data.");
                            continue;
                        }

                        // Validate Victim (Person)
                        Person person = CommandHelpers.GetPersonByName(context, victim);

                        if (person == null)
                        {
                            Console.WriteLine("Error: Invalid data.");
                            continue;
                        }

                        // Add victim to anomaly
                        anomalyEntity.Victims.Add(person);

                        // Success notification
                        Console.WriteLine($"Successfully imported anomaly.");
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
