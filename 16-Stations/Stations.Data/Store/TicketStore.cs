namespace Stations.Data.Store
{
    using Models;
    using Models.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class TicketStore
    {
        public static void AddTickets(List<TicketImportDto> ticketDtos)
        {
            using (var context = new StationsContext())
            {
                foreach (var ticketDto in ticketDtos)
                {
                    // Validate input
                    Trip trip = context.Trips
                        .FirstOrDefault(t =>
                            t.OriginStation.Name == ticketDto.OriginStation &&
                            t.DestinationStation.Name == ticketDto.DestinationStation &&
                            t.DepartureTime == ticketDto.DepartureTime);
                    CustomerCard card = context.CustomerCards
                        .FirstOrDefault(c => c.Name == ticketDto.CardName);
                    SeatingClass seatingClass = context.SeatingClasses
                        .FirstOrDefault(c => c.Abbreviation == ticketDto.Seat.Substring(0, 2));

                    if (trip == null || card == null || seatingClass == null)
                    {
                        Console.WriteLine("Invalid data format.");
                        continue;
                    }
                    // Validate train seats
                    var trainSeats = context.TrainSeats
                        .FirstOrDefault(ts => ts.TrainId == trip.TrainId && ts.SeatingClassId == seatingClass.Id);
                    if (trainSeats == null)
                    {
                        Console.WriteLine("Invalid data format.");
                        continue;
                    }
                    // Validate seat number
                    int seatNumber = int.Parse(ticketDto.Seat.Substring(2));
                    if (seatNumber < 0 || seatNumber > trainSeats.Quantity)
                    {
                        Console.WriteLine("Invalid data format.");
                        continue;
                    }

                    // Add ticket to DB
                    Ticket ticket = new Ticket
                    {
                        TripId = trip.Id,
                        Price = ticketDto.Price,
                        SeatingPlace = ticketDto.Seat,
                        PersonalCardId = card.Id
                    };
                    context.Tickets.Add(ticket);
                    context.SaveChanges();

                    // Success Notification
                    Console.WriteLine($"Ticket from {ticketDto.OriginStation} to {ticketDto.DestinationStation} departing at {ticketDto.DepartureTime} imported.");
                }
            }
        }
    }
}
