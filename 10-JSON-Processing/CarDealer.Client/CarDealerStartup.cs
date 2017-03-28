namespace CarDealer.Client
{
    using Data;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class CarDealerStartup
    {
        static void Main(string[] args)
        {
            InitializeDatabase();
            SeedData();

            RunSolutions();
        }

        private static void RunSolutions()
        {
            while (true)
            {
                Console.Write("Solutions to CarDealer:\n" +
                "1. Ordered Customers\n" +
                "2. Cars from make Toyota\n" +
                "3. Local Suppliers\n" +
                "4. Cars with Their List of Parts\n" +
                "5. Total Sales by Customer\n" +
                "6. Sales with Applied Discount\n" +
                "Enter option or [end]: ");

                string option = Console.ReadLine();
                bool isValidOption = true;
                switch (option)
                {
                    case "1": OrderedCustomers(); break;
                    case "2": CarsFromToyota(); break;
                    case "3": LocalSuppliers(); break;
                    case "4": CarsWithListOfParts(); break;
                    case "5": TotalSalesByCustomer(); break;
                    case "6": SalesWithAppliedDiscount(); break;
                    case "end": return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option.");
                        isValidOption = false; break;
                }
                if (isValidOption)
                {
                    Console.Write("Continue with the next solution...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private static void SalesWithAppliedDiscount()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                var sales = context.Sales
                    .Select(s => new
                    {
                        Car = new
                        {
                            Make = s.Car.Make,
                            Model = s.Car.Model,
                            TravelledDistance = s.Car.TravelledDistance
                        },
                        CustomerName = s.Customer.Name,
                        Discount = s.Discount,
                        Price = s.Car.Parts.Sum(p => p.Price),
                        PriceWithDiscount = s.Car.Parts.Sum(p => p.Price) * (1 - s.Discount)
                    });
                string salesJson = JsonConvert.SerializeObject(sales, Formatting.Indented);
                Console.WriteLine(salesJson);
            }
        }

        private static void TotalSalesByCustomer()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                var customers = context.Customers
                    .Where(c => c.Sales.Count > 0)
                    .Select(c => new
                    {
                        FullName = c.Name,
                        BoughtCars = c.Sales.Count(),
                        SpentMoney = c.Sales.Sum(s =>
                                    s.Car.Parts.Sum(p => p.Price)   // car price = sum of all parts
                                    * (1 - s.Discount))             // after discount

                    })
                    .OrderByDescending(ts => ts.SpentMoney)
                    .ThenByDescending(ts => ts.BoughtCars);
                string customersJson = JsonConvert.SerializeObject(customers, Formatting.Indented);
                Console.WriteLine(customersJson);
            }
        }

        private static void CarsWithListOfParts()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                var cars = context.Cars
                    //.Include("Parts")
                    .Select(c => new
                    {
                        Car = new
                        {
                            Make = c.Make,
                            Model = c.Model,
                            TravelledDistance = c.TravelledDistance
                        },
                        Parts = c.Parts.Select(p => new
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                    });
                string carsJson = JsonConvert.SerializeObject(cars, Formatting.Indented);
                Console.WriteLine(carsJson);
            }
        }

        private static void LocalSuppliers()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                var suppliers = context.Suppliers
                    .Where(s => s.IsImporter == false)
                    .Select(s => new
                    {
                        Id = s.Id,
                        Name = s.Name,
                        PartsCount = s.Parts.Count()
                    });
                string suppliersJson = JsonConvert.SerializeObject(suppliers, Formatting.Indented);
                Console.WriteLine(suppliersJson);
            }
        }

        private static void CarsFromToyota()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                var cars = context.Cars
                    .Where(c => c.Make == "Toyota")
                    .OrderBy(c => c.Model)
                    .ThenByDescending(c => c.TravelledDistance)
                    .Select(c => new
                    {
                        Id = c.Id,
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance
                    });
                string carsJson = JsonConvert.SerializeObject(cars, Formatting.Indented);
                Console.WriteLine(carsJson);
            }
        }

        private static void OrderedCustomers()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                var customers = context.Customers
                    //.Include("Sales")
                    //.Include("Cars")
                    .OrderBy(c => c.BirthDate)
                    .ThenBy(c => c.IsYoungDriver) // false first => ASC
                    .Select(c => new
                    {
                        Id = c.Id,
                        Name = c.Name,
                        BirthDate = c.BirthDate,
                        IsYoungDriver = c.IsYoungDriver,
                        Sales = context.Sales
                                .Where(s => s.CustomerId == c.Id)
                                .Select(s => new
                                {
                                    Car = s.Car.Make + " " + s.Car.Model,
                                    Discount = s.Discount
                                })
                    });
                string customersJson = JsonConvert.SerializeObject(customers, Formatting.Indented);
                Console.WriteLine(customersJson);
            }
        }

        private static void ImportSales()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                Console.WriteLine("Seeding Sales");

                List<Sale> sales = new List<Sale>();
                int[] discounts = { 0, 5, 10, 15, 20, 30, 40, 50 };
                int carsCount = context.Cars.Count();
                int customersCount = context.Customers.Count();
                int discountsCount = discounts.Count();

                for (int i = 0; i < 15; i++)
                {
                    Sale sale = new Sale()
                    {
                        CarId = i * 3 % carsCount + 1,
                        CustomerId = i * 5 % customersCount + 1,
                        Discount = discounts[i * 7 % discountsCount] / 100m
                    };
                    sales.Add(sale);
                }
                context.Sales.AddRange(sales);
                context.SaveChanges();
            }
        }

        private static void ImportCustomers()
        {
            Console.WriteLine("Seeding Customers");

            using (CarDealerContext context = new CarDealerContext())
            {
                string json = File.ReadAllText("../../Import/customers.json");
                List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(json);
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }
        }

        private static void ImportCars()
        {
            Console.WriteLine("Seeding Cars");

            using (CarDealerContext context = new CarDealerContext())
            {
                string json = File.ReadAllText("../../Import/cars.json");
                List<Car> cars = JsonConvert.DeserializeObject<List<Car>>(json);

                int partsCount = context.Parts.Count();
                int index = 0;
                foreach (var car in cars)
                {
                    int currentCount = ++index % 20 + 1;
                    if (currentCount < 10) currentCount += 10;

                    for (int j = 0; j < currentCount; j++)
                    {
                        car.Parts.Add(context.Parts.Find((++index + j * 2) % partsCount + 1));
                    }
                }
                context.Cars.AddRange(cars);
                context.SaveChanges();
            }
        }

        private static void ImportParts()
        {
            Console.WriteLine("Seeding Parts");

            using (CarDealerContext context = new CarDealerContext())
            {
                string json = File.ReadAllText("../../Import/parts.json");
                List<Part> parts = JsonConvert.DeserializeObject<List<Part>>(json);

                int suppliersCount = context.Suppliers.Count();
                int index = 0;

                foreach (Part part in parts)
                {
                    part.SupplierId = (index++ * 27) % suppliersCount + 1;
                }
                context.Parts.AddRange(parts);
                context.SaveChanges();
            }
        }

        private static void ImportSuppliers()
        {
            Console.WriteLine("Seeding Suppliers");

            using (CarDealerContext context = new CarDealerContext())
            {
                string json = File.ReadAllText("../../Import/suppliers.json");
                List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(json);
                context.Suppliers.AddRange(suppliers);
                context.SaveChanges();
            }
        }

        private static void SeedData()
        {
            ImportSuppliers();
            ImportParts();
            ImportCars();
            ImportCustomers();
            ImportSales();
            Console.WriteLine();
        }

        private static void InitializeDatabase()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                Console.WriteLine("Initializing database [CarDealer]");
                context.Database.Initialize(true);
            }
        }
    }
}
