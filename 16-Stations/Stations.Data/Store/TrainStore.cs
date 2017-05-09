namespace Stations.Data.Store
{
    using Models;
    using Models.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class TrainStore
    {
        public static void ImportTrains(IEnumerable<TrainImportDto> trainDtos)
        {
            using (var context = new StationsContext())
            {
                foreach (var trainDto in trainDtos)
                {
                    // Validate TrainNumber
                    if (trainDto.TrainNumber == null ||
                        trainDto.TrainNumber.Length > 10 ||
                        context.Trains.Any(t => t.TrainNumber == trainDto.TrainNumber))
                    {
                        Console.WriteLine("Invalid data format.");
                        continue;
                    }
                    // Validate TrainType
                    if (trainDto.Type == null)
                    {
                        Console.WriteLine("Invalid data format.");
                        continue;
                    }
                    // Validate Seats
                    bool isValidEntity = true;
                    foreach (var seatDto in trainDto.Seats)
                    {
                        if (seatDto.Name == null ||
                            seatDto.Abbreviation == null ||
                            seatDto.Quantity == null ||
                            seatDto.Quantity < 0 ||
                            !context.SeatingClasses.Any(c => c.Name == seatDto.Name && c.Abbreviation == seatDto.Abbreviation))
                        {
                            isValidEntity = false;
                            Console.WriteLine("Invalid data format.");
                            break;
                        }
                    }
                    // Ignore invalid entities
                    if (!isValidEntity)
                    {
                        continue;
                    }

                    // Add train & train seats to DB
                    Train train = new Train
                    {
                        TrainNumber = trainDto.TrainNumber,
                        Type = (TrainType)trainDto.Type,
                        TrainSeats = new List<TrainSeat>()
                    };

                    foreach (var seatDto in trainDto.Seats)
                    {
                        train.TrainSeats.Add(new TrainSeat
                        {
                            SeatingClassId = context.SeatingClasses.FirstOrDefault(c => c.Name == seatDto.Name).Id,
                            Quantity = (int)seatDto.Quantity
                        });
                    }

                    context.Trains.Add(train);
                    context.SaveChanges();

                    // Success Notification
                    Console.WriteLine($"Record {trainDto.TrainNumber} successfully imported.");
                }
            }
        }
    }
}
