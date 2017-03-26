namespace Demo.Migrations
{
    using Data;
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<QueryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(QueryContext context)
        {
            //  This method will be called after migrating to the latest version.
            Console.WriteLine("Seeding data");

            SeedClients(context);
            SeedProducts(context);
            SeedStorages(context);
            SeedOrders(context);
            SeedProductStocks(context);

            base.Seed(context);
        }

        private static void SeedProductStocks(QueryContext context)
        {

            context.ProductStocks.AddOrUpdate(ps => new { ps.ProductId, ps.StorageId },
                new ProductStock() { ProductId = 1, StorageId = 1, Quantity = 100 },
                new ProductStock() { ProductId = 1, StorageId = 2, Quantity = 200 },
                new ProductStock() { ProductId = 1, StorageId = 3, Quantity = 300 },
                new ProductStock() { ProductId = 2, StorageId = 1, Quantity = 10 },
                new ProductStock() { ProductId = 2, StorageId = 2, Quantity = 20 },
                new ProductStock() { ProductId = 2, StorageId = 3, Quantity = 30 }
                );
            context.SaveChanges();
        }

        private static void SeedOrders(QueryContext context)
        {
            context.Orders.AddOrUpdate(o => o.Id,
                new Order() { ClientId = context.Clients.Find(1).Id },
                new Order() { ClientId = context.Clients.Find(2).Id },
                new Order() { ClientId = context.Clients.Find(3).Id },
                new Order() { ClientId = context.Clients.Find(4).Id },
                new Order() { ClientId = context.Clients.Find(5).Id }
                );
            context.SaveChanges();

            context.OrderProduts.AddOrUpdate(op => new { op.OrderId, op.ProductId },
                new OrderProduct() { OrderId = 1, ProductId = 1, Quantity = 1 },
                new OrderProduct() { OrderId = 1, ProductId = 2, Quantity = 1 },
                new OrderProduct() { OrderId = 2, ProductId = 1, Quantity = 2 },
                new OrderProduct() { OrderId = 3, ProductId = 2, Quantity = 3 },
                new OrderProduct() { OrderId = 4, ProductId = 2, Quantity = 10 },
                new OrderProduct() { OrderId = 4, ProductId = 1, Quantity = 10 }
                );
            context.SaveChanges();
        }

        private static void SeedProducts(QueryContext context)
        {
            context.Products.AddOrUpdate(p => p.Name,
                new Product() { Name = "Oil Pump", Cost = 1000m },
                new Product() { Name = "Tesla Model S", Cost = 100000m }
                );
            context.SaveChanges();
        }

        private static void SeedClients(QueryContext context)
        {
            context.Clients.AddOrUpdate(c => c.Name,
                new Client() { Name = "Petar Ivanov", Address = "Sofia" },
                new Client() { Name = "Ivan Petrov", Address = "Plovdiv" },
                new Client() { Name = "Maria", Address = "Plovdiv" },
                new Client() { Name = "Ina" },
                new Client() { Name = "Gosho" }
                );
            context.SaveChanges();
        }

        private static void SeedStorages(QueryContext context)
        {
            context.Storages.AddOrUpdate(s => new { s.Name, s.Location },
                new Storage() { Name = "Technopolis", Location = "Sofia" },
                new Storage() { Name = "Plesio", Location = "Sofia" },
                new Storage() { Name = "JarComputers", Location = "Sofia" }
                );
            context.SaveChanges();
        }
    }
}
