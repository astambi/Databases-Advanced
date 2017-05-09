namespace Stations.Data.Store
{
    using Models;
    using Models.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CardStore
    {
        public static void AddCards(List<CustomerCard> cards)
        {
            using (var context = new StationsContext())
            {
                foreach (var card in cards)
                {
                    // Validate input
                    if (card.Name == null || card.Name.Length > 128 ||
                        card.Age < 0 || card.Age > 120)
                    {
                        Console.WriteLine("Invalid data format.");
                        continue;
                    }

                    // Add Card to DB
                    context.CustomerCards.Add(card);
                    context.SaveChanges();

                    // Success Notification
                    Console.WriteLine($"Record {card.Name} successfully imported.");
                }
            }
        }
    }
}
