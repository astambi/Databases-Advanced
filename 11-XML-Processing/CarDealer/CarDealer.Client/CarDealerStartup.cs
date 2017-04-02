namespace CarDealer.Client
{
    using Data;
    using Models;
    using System;
    using System.Linq;
    using System.Xml.Linq;

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
                "1. Cars\n" +
                "2. Cars from make Ferrari\n" +
                "3. Local Suppliers\n" +
                "4. Cars with Their List of Parts\n" +
                "5. Total Sales by Customer\n" +
                "6. Sales with Applied Discount\n" +
                "Enter option or [end]: ");

                string option = Console.ReadLine();
                bool isValidOption = true;
                switch (option)
                {
                    case "1": Cars(); break;
                    case "2": CarsFromFerrari(); break;
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

        private static void ExportAndPrint(XDocument xmlDoc, string fileName) // filename without extentsion
        {
            Console.WriteLine(xmlDoc);
            xmlDoc.Save($"../../Export/{fileName}.xml");
            xmlDoc.Save($"../../Export/{fileName}NoFormatting.xml", SaveOptions.DisableFormatting);
            Console.WriteLine($"\nXML Document exported as: \nExport/{fileName}.xml & Export/{fileName}NoFormatting.xml\n");
        }

        private static void SalesWithAppliedDiscount()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                var sales = context.Sales
                    .Select(s => new
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance,
                        CustomerName = s.Customer.Name,
                        Discount = s.Discount,
                        Price = s.Car.Parts.Sum(p => p.Price),
                        PriceWithDiscount = s.Car.Parts.Sum(p => p.Price) * (1 - s.Discount)
                    });

                // Create XML Document
                XDocument xmlDoc = new XDocument();
                XElement salesXml = new XElement("sales");

                foreach (var sale in sales)
                {
                    XElement saleXml = new XElement("sale");
                    XElement carXml = new XElement("car",
                        new XAttribute("make", sale.Make),
                        new XAttribute("model", sale.Model),
                        new XAttribute("travelled-distance", sale.TravelledDistance)
                        );
                    XElement customerXml = new XElement("customer-name", sale.CustomerName);
                    XElement discountXml = new XElement("discount", sale.Discount);
                    XElement priceXml = new XElement("price", sale.Price);
                    XElement priceWithDiscountXml = new XElement("price-with-discount", sale.PriceWithDiscount);
                    saleXml.Add(carXml, customerXml, discountXml, priceXml, priceWithDiscountXml);
                    
                    // Add sale to sales
                    salesXml.Add(saleXml);
                }
                xmlDoc.Add(salesXml);

                // Export & Print
                ExportAndPrint(xmlDoc, "sales-discounts");
            }
        }

        private static void TotalSalesByCustomer()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                // Customers total sales
                var customersSales = context.Customers
                    .Where(c => c.Sales.Any())
                    .Select(c => new
                    {
                        FullName = c.Name,
                        BoughtCars = c.Sales.Count(),
                        SpentMoney = c.Sales.Sum(s =>
                                    s.Car.Parts.Sum(p => p.Price)   // carPrice = Sum of parts
                                    * (1 - s.Discount))             // less discount
                    })
                    .OrderByDescending(c => c.SpentMoney)
                    .ThenByDescending(c => c.BoughtCars);

                // Create XML Document
                XDocument xmlDoc = new XDocument();
                XElement customersXml = new XElement("customers");

                foreach (var customer in customersSales)
                {
                    XElement customerXml = new XElement("customer",
                        new XAttribute("full-name", customer.FullName),
                        new XAttribute("bought-cars", customer.BoughtCars),
                        new XAttribute("spent-money", customer.SpentMoney));
                    customersXml.Add(customerXml);
                }
                xmlDoc.Add(customersXml);

                // Export & Print
                ExportAndPrint(xmlDoc, "customers-total-sales");
            }
        }

        private static void CarsWithListOfParts()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                // Cars with Parts
                var carsWithParts = context.Cars
                    .Select(c => new
                    {
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance,
                        Parts = c.Parts.Select(p => new
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                    });

                // Create XML Document
                XDocument xmlDoc = new XDocument();
                XElement carsXml = new XElement("cars");

                foreach (var car in carsWithParts)
                {
                    // car
                    XElement carXml = new XElement("car",
                        new XAttribute("make", car.Make),
                        new XAttribute("model", car.Model),
                        new XAttribute("travelled-distance", car.TravelledDistance));
                    // car parts
                    XElement partsXml = new XElement("parts");
                    foreach (var part in car.Parts)
                    {
                        XElement partXml = new XElement("part",
                            new XAttribute("name", part.Name),
                            new XAttribute("price", part.Price));
                        partsXml.Add(partXml);
                    }
                    carXml.Add(partsXml);

                    // Add car to cars
                    carsXml.Add(carXml);
                }
                xmlDoc.Add(carsXml);

                // Export & Print
                ExportAndPrint(xmlDoc, "cars-and-parts");
            }
        }

        private static void LocalSuppliers()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                // Local Suppliers
                var suppliers = context.Suppliers
                    .Where(s => s.IsImporter == false)
                    .Select(s => new
                    {
                        Id = s.Id,
                        Name = s.Name,
                        PartsCount = s.Parts.Count()
                    });

                // Create XML Document
                XDocument xmlDoc = new XDocument();
                XElement suppliersXml = new XElement("suppliers");

                foreach (var supplier in suppliers)
                {
                    XElement supplierXml = new XElement("supplier",
                        new XAttribute("id", supplier.Id),
                        new XAttribute("name", supplier.Name),
                        new XAttribute("parts-count", supplier.PartsCount));
                    suppliersXml.Add(supplierXml);
                }
                xmlDoc.Add(suppliersXml);

                // Export & Print
                ExportAndPrint(xmlDoc, "local-suppliers");
            }
        }

        private static void CarsFromFerrari()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                // Cars from make Ferrari
                var cars = context.Cars
                    .Where(c => c.Make == "Ferrari")
                    .OrderBy(c => c.Model)
                    .ThenByDescending(c => c.TravelledDistance)
                    .Select(c => new
                    {
                        Id = c.Id,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance
                    });

                // Create XML Document
                XDocument xmlDoc = new XDocument();
                XElement carsXml = new XElement("cars");
                foreach (var car in cars)
                {
                    XElement carXml = new XElement("car",
                        new XAttribute("id", car.Id),
                        new XAttribute("model", car.Model),
                        new XAttribute("travelled-distance", car.TravelledDistance));
                    carsXml.Add(carXml);
                }
                xmlDoc.Add(carsXml);

                // Export & Print
                ExportAndPrint(xmlDoc, "ferrari-cars");
            }
        }

        private static void Cars()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                var cars = context.Cars
                    .Where(c => c.TravelledDistance > 2000000L)
                    .OrderBy(c => c.Make)
                    .ThenBy(c => c.Model)
                    .Select(c => new
                    {
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance
                    });

                // Create XML Document
                XDocument xmlDoc = new XDocument();
                XElement carsXml = new XElement("cars");

                foreach (var car in cars)
                {
                    XElement carXml = new XElement("car");
                    XElement makeXml = new XElement("make", car.Make);
                    XElement modelXml = new XElement("model", car.Model);
                    XElement distanceXml = new XElement("travelled-distance", car.TravelledDistance);
                    carXml.Add(makeXml, modelXml, distanceXml);

                    carsXml.Add(carXml);
                }
                xmlDoc.Add(carsXml);

                // Export & Print
                ExportAndPrint(xmlDoc, "cars");
            }
        }

        private static void SeedSales()
        {
            Console.WriteLine("Seeding Sales (with random customer, car & discount)");
            using (CarDealerContext context = new CarDealerContext())
            {
                int[] discounts = { 0, 5, 10, 15, 20, 30, 40, 50 };
                int discountsCount = discounts.Count();
                int carsCount = context.Cars.Count();
                int customersCount = context.Customers.Count();
                int salesCount = 200;
                Random rnd = new Random();

                for (int i = 0; i < salesCount; i++)
                {
                    int customerId = rnd.Next(1, customersCount + 1);       // random customer
                    int carId = rnd.Next(1, carsCount + 1);                 // random car
                    int discount = discounts[rnd.Next(0, discountsCount)];  // random discount

                    // Add additional discount for young drivers
                    bool isYoungDriver = context.Customers.Find(customerId).IsYoungDriver;
                    if (isYoungDriver) discount += 5;

                    Sale newSale = new Sale()
                    {
                        CarId = carId,              // random car
                        CustomerId = customerId,    // random customer
                        Discount = discount / 100m  // random discount + discount for young driver (if elligible)
                    };
                    context.Sales.Add(newSale);
                }
                context.SaveChanges();
            }
        }

        private static void SeedCustomers()
        {
            Console.WriteLine("Seeding Customers");
            using (CarDealerContext context = new CarDealerContext())
            {
                XDocument xmlDoc = XDocument.Load("../../Import/customers.xml");
                var customers = xmlDoc.Root.Elements();

                foreach (XElement customer in customers)
                {
                    Customer newCustomer = new Customer()
                    {
                        Name = customer.Attribute("name").Value,
                        BirthDate = DateTime.Parse(customer.Element("birth-date").Value),
                        IsYoungDriver = bool.Parse(customer.Element("is-young-driver").Value)
                    };
                    context.Customers.Add(newCustomer);
                }
                context.SaveChanges();
            }
        }

        private static void SeedCars()
        {
            Console.WriteLine("Seeding Cars (with random 10 to 20 parts)");
            using (CarDealerContext context = new CarDealerContext())
            {
                int partsCount = context.Parts.Count();

                XDocument xmlDoc = XDocument.Load("../../Import/cars.xml");
                var cars = xmlDoc.Root.Elements();
                Random rnd = new Random();

                foreach (XElement car in cars)
                {
                    Car newCar = new Car()
                    {
                        Make = car.Element("make").Value,
                        Model = car.Element("model").Value,
                        TravelledDistance = long.Parse(car.Element("travelled-distance").Value)
                    };
                    // Add between 10 and 20 random parts to a car
                    int carPartsCount = rnd.Next(10, 21);
                    for (int i = 0; i < carPartsCount; i++)             // random number of parts [10; 20]
                    {
                        int partIndex = rnd.Next(1, partsCount + 1);    // random part index
                        newCar.Parts.Add(context.Parts.Find(partIndex));
                    }
                    context.Cars.Add(newCar);
                }
                context.SaveChanges();
            }
        }

        private static void SeedParts()
        {
            Console.WriteLine("Seeding Parts (with random supplier)");
            using (CarDealerContext context = new CarDealerContext())
            {
                int suppliersCount = context.Suppliers.Count();

                XDocument xmlDoc = XDocument.Load("../../Import/parts.xml");
                var parts = xmlDoc.Root.Elements();
                Random rnd = new Random();

                foreach (XElement part in parts)
                {
                    Part newPart = new Part()
                    {
                        Name = part.Attribute("name").Value,
                        Price = decimal.Parse(part.Attribute("price").Value),
                        Quantity = int.Parse(part.Attribute("quantity").Value),
                        SupplierId = rnd.Next(1, suppliersCount + 1)    // random supplier
                    };
                    context.Parts.Add(newPart);
                }
                context.SaveChanges();
            }
        }

        private static void SeedSuppliers()
        {
            Console.WriteLine("Seeding Suppliers");
            using (CarDealerContext context = new CarDealerContext())
            {
                XDocument xmlDoc = XDocument.Load("../../Import/suppliers.xml");
                var suppliers = xmlDoc.Root.Elements();

                foreach (XElement supplier in suppliers)
                {
                    Supplier newSupplier = new Supplier()
                    {
                        Name = supplier.Attribute("name")?.Value,
                        IsImporter = bool.Parse(supplier.Attribute("is-importer")?.Value)
                    };
                    context.Suppliers.Add(newSupplier);
                }
                context.SaveChanges();
            }
        }

        private static void SeedData()
        {
            SeedSuppliers();
            SeedParts();
            SeedCars();
            SeedCustomers();
            SeedSales();
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
