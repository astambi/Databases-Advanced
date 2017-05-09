namespace Stations.Data.Store
{
    using Models;
    using Models.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public static class TripStore
    {
        public static void AddTrips(IEnumerable<TripImportDto> tripDtos)
        {
            using (var context = new StationsContext())
            {
                string dateFormat = "dd/MM/yyyy HH:mm";
                string timeDiffFormat = "g";

                foreach (var tripDto in tripDtos)
                {
                    // Validate required input
                    if (tripDto.OriginStation == null ||
                        tripDto.DestinationStation == null ||
                        tripDto.DepartureTime == null ||
                        tripDto.ArrivalTime == null ||
                        tripDto.Train == null)
                    {
                        Console.WriteLine("Invalid data format.");
                        continue;
                    }
                    // Validate Train, origin & destination Stations
                    Train train = context.Trains.FirstOrDefault(t => t.TrainNumber == tripDto.Train);
                    Station originStation = context.Stations.FirstOrDefault(s => s.Name == tripDto.OriginStation);
                    Station destinationStation = context.Stations.FirstOrDefault(s => s.Name == tripDto.DestinationStation);
                    if (train == null || originStation == null || destinationStation == null)
                    {
                        Console.WriteLine("Invalid data format.");
                        continue;
                    }
                    // Validate Departure time < Arrival time
                    DateTime departureTime = DateTime.ParseExact(tripDto.DepartureTime, dateFormat, CultureInfo.InvariantCulture);
                    DateTime arrivalTime = DateTime.ParseExact(tripDto.ArrivalTime, dateFormat, CultureInfo.InvariantCulture);
                    if (departureTime >= arrivalTime)
                    {
                        Console.WriteLine("Invalid data format.");
                        continue;
                    }

                    TimeSpan timeDifference = TimeSpan.ParseExact(tripDto.TimeDifference ?? "00:00", timeDiffFormat, CultureInfo.InvariantCulture);

                    // Add trip to DB
                    Trip trip = new Trip
                    {
                        TrainId = train.Id,
                        OriginStationId = originStation.Id,
                        DestinationStationId = destinationStation.Id,
                        DepartureTime = departureTime,
                        ArrivalTime = arrivalTime,
                        Status = tripDto.Status,
                        TimeDifference = timeDifference         
                    };
                    context.Trips.Add(trip);
                    context.SaveChanges();

                    // Success Notification
                    Console.WriteLine($"Trip from {tripDto.OriginStation} to {tripDto.DestinationStation} imported.");
                }
            }
        }
    }
}
