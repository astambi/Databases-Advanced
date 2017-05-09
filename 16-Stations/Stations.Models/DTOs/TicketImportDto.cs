namespace Stations.Models.DTOs
{
    using System;

    public class TicketImportDto
    {
        public decimal Price { get; set; }
        public string Seat { get; set; }
        public string OriginStation { get; set; }
        public string DestinationStation { get; set; }
        public DateTime DepartureTime { get; set; }
        public string CardName { get; set; }
    }
}
