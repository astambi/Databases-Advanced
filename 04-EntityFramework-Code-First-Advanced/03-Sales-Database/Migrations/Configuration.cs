namespace _03_Sales_Database.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<_03_Sales_Database.SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true; // true for Problem 6
            AutomaticMigrationDataLossAllowed = true; // enable for Problem 6
            ContextKey = "_03_Sales_Database.SalesContext";
        }

        protected override void Seed(SalesContext context)
        {
            //  This method will be called after migrating to the latest version.

            // Products
            context.Products.AddOrUpdate(p => p.Name,
                new Product()
                {
                    Name = "Tesla",
                    Quantity = 1,
                    Price = 100000m
                },
                new Product()
                {
                    Name = "Samsung Galaxy S7",
                    Quantity = 1,
                    Price = 1500m
                },
                new Product()
                {
                    Name = "Book",
                    Quantity = 1,
                    Price = 25m
                },
                new Product()
                {
                    Name = "Lenovo Y50 Laptop",
                    Quantity = 1,
                    Price = 2500m
                }, new Product()
                {
                    Name = "Concert Alex Penda",
                    Quantity = 1,
                    Price = 100m
                });

            // Customers

            // Uncomment for Problems 3, 4, 5
            // Disable for Problem 6 !
            //context.Customers.AddOrUpdate(c => c.Email,
            //    new Customer()
            //    {
            //        Name = "Tom",
            //        Email = "tom@gmail.com",
            //        CreditCardNumber = "123456789123456"
            //    },
            //    new Customer()
            //    {
            //        Name = "Tea",
            //        Email = "tea@gmail.com",
            //        CreditCardNumber = "789456123111111"
            //    },
            //    new Customer()
            //    {
            //        Name = "Nia",
            //        Email = "nia@gmail.com",
            //        CreditCardNumber = "777888999111222"
            //    },
            //    new Customer()
            //    {
            //        Name = "Ralph",
            //        Email = "ralph@gmail.com",
            //        CreditCardNumber = "666666666666666"
            //    },
            //    new Customer()
            //    {
            //        Name = "Steve",
            //        Email = "steve@gmail.com",
            //        CreditCardNumber = "4444455555556666"
            //    });

            // Uncomment for Problem 6
            context.Customers.AddOrUpdate(c => c.Email,
                new Customer()
                {
                    FirstName = "Tom",
                    LastName = "Edvards",
                    Email = "tom@gmail.com",
                    CreditCardNumber = "123456789123456"
                },
                new Customer()
                {
                    FirstName = "Tea",
                    LastName = "Nielsen",
                    Email = "tea@gmail.com",
                    CreditCardNumber = "789456123111111"
                },
                new Customer()
                {
                    FirstName = "Nia",
                    LastName = "Barnes",
                    Email = "nia@gmail.com",
                    CreditCardNumber = "777888999111222"
                },
                new Customer()
                {
                    FirstName = "Ralph",
                    LastName = "Finnes",
                    Email = "ralph@gmail.com",
                    CreditCardNumber = "666666666666666"
                },
                new Customer()
                {
                    FirstName = "Steve",
                    LastName = "Jobs",
                    Email = "steve@gmail.com",
                    CreditCardNumber = "4444455555556666"
                });

            // StoreLocations
            context.StoreLocations.AddOrUpdate(l => l.LocationName,
                new StoreLocation() { LocationName = "Sofia" },
                new StoreLocation() { LocationName = "Lisboa" },
                new StoreLocation() { LocationName = "Barcelona" },
                new StoreLocation() { LocationName = "Munchen" },
                new StoreLocation() { LocationName = "Rotterdam" });

            context.SaveChanges();

            // Sales
            context.Sales.AddOrUpdate(s => s.Id,
                new Sale()
                {
                    Product = context.Products.FirstOrDefault(p => p.Name.StartsWith("Tesla")),
                    Customer = context.Customers.FirstOrDefault(c => c.Email.StartsWith("steve")),
                    Date = DateTime.Now,
                    StoreLocation = context.StoreLocations.FirstOrDefault(l => l.LocationName == "Sofia")
                },
                new Sale()
                {
                    Product = context.Products.FirstOrDefault(p => p.Name.StartsWith("Samsung")),
                    Customer = context.Customers.FirstOrDefault(c => c.Email.StartsWith("ralph")),
                    Date = DateTime.Now,
                    StoreLocation = context.StoreLocations.FirstOrDefault(l => l.LocationName == "Lisboa")
                },
                new Sale()
                {
                    Product = context.Products.FirstOrDefault(p => p.Name.StartsWith("Book")),
                    Customer = context.Customers.FirstOrDefault(c => c.Email.StartsWith("nia")),
                    Date = DateTime.Now,
                    StoreLocation = context.StoreLocations.FirstOrDefault(l => l.LocationName == "Barcelona")
                },
                new Sale()
                {
                    Product = context.Products.FirstOrDefault(p => p.Name.StartsWith("Lenovo")),
                    Customer = context.Customers.FirstOrDefault(c => c.Email.StartsWith("tea")),
                    Date = DateTime.Now,
                    StoreLocation = context.StoreLocations.FirstOrDefault(l => l.LocationName == "Rotterdam")
                },
                new Sale()
                {
                    Product = context.Products.FirstOrDefault(p => p.Name.StartsWith("Concert")),
                    Customer = context.Customers.FirstOrDefault(c => c.Email.StartsWith("tom")),
                    Date = DateTime.Now,
                    StoreLocation = context.StoreLocations.FirstOrDefault(l => l.LocationName == "Munchen")
                });

            base.Seed(context);
        }
    }
}
