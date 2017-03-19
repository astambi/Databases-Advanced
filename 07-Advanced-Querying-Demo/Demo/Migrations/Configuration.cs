namespace Demo.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.QueryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.QueryContext context)
        {
            //  This method will be called after migrating to the latest version.
            Console.WriteLine("Seeding data\n");

            // Clients
            context.Clients.AddOrUpdate(c => c.Name,
                new Client() { Name = "Petar Ivanov", Address = "Sofia" },
                new Client() { Name = "Ivan Petrov", Address = "Plovdiv" },
                new Client() { Name = "Maria", Address = "Plovdiv" },
                new Client() { Name = "Ina" },
                new Client() { Name = "Gosho" }
                );

            // Products
            context.Products.AddOrUpdate(p => p.Name,
                new Product() { Name = "Oil Pump" },
                new Product() { Name = "Tesla Model S" }
                );
            context.SaveChanges();

            // Orders
            //context.Orders.AddOrUpdate(o => new { o.ClientId },
            //    new Order()
            //    {
            //        ClientId = context.Clients
            //                    .FirstOrDefault().Id,
            //        Products = new[] { context.Products.Find(1) }
            //    },
            //    new Order()
            //    {
            //        ClientId = context.Clients
            //                    .OrderBy(c => c.Id).Skip(1)
            //                    .FirstOrDefault().Id,
            //        Products = new[] { context.Products.Find(2) }
            //    },
            //    new Order()
            //    {
            //        ClientId = context.Clients
            //                    .OrderBy(c => c.Id).Skip(2)
            //                    .FirstOrDefault().Id,
            //        Products = new[] {
            //                    context.Products.Find(1),
            //                    context.Products.Find(2)}
            //    },
            //    new Order()
            //    {
            //        ClientId = context.Clients
            //                    .OrderBy(c => c.Id).Skip(3)
            //                    .FirstOrDefault().Id,
            //        Products = new[] {
            //                    context.Products.Find(1),
            //                    context.Products.Find(2)}
            //    }
            //    );
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
