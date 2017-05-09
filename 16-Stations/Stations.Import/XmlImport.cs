namespace Stations.Import
{
    using Data.Store;
    using Models;
    using Models.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;

    public static class XmlImport
    {
        public static void ImportPersonCards()
        {
            XDocument xmlDoc = LoadXmlFile("cards");
            var cards = xmlDoc.Root.Elements()
                .Select(e => new CustomerCard
                {
                    Name = e.Element("Name").Value,
                    Age = int.Parse(e.Element("Age").Value),
                    CardType = (CardType)Enum.Parse(typeof(CardType), e.Element("CardType")?.Value ?? "Normal", true)
                }).ToList();
            CardStore.AddCards(cards);
        }

        internal static void ImportTickets()
        {
            var tickets = LoadXmlFile("tickets").Root.Elements();

            List<TicketImportDto> ticketDtos = new List<TicketImportDto>();
            foreach (var ticket in tickets)
            {
                TicketImportDto ticketDto = new TicketImportDto
                {
                    Price = decimal.Parse(ticket.Attribute("price").Value),
                    Seat = ticket.Attribute("seat").Value,
                    OriginStation = ticket.Element("Trip").Element("OriginStation").Value,
                    DestinationStation = ticket.Element("Trip").Element("DestinationStation").Value,
                    DepartureTime = DateTime.ParseExact(ticket.Element("Trip").Element("DepartureTime").Value, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                };
                if (ticket.Element("Card") != null)
                {
                    ticketDto.CardName = ticket.Element("Card").Attribute("Name").Value;
                }
                ticketDtos.Add(ticketDto);
            }
            TicketStore.AddTickets(ticketDtos);
        }

        private static XDocument LoadXmlFile(string fileName)
        {
            return XDocument.Load($"../../Import/{fileName}.xml");
        }
    }
}
