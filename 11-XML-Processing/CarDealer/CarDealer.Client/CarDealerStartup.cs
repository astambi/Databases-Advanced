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

        private static void ExportToFileAndPrint(XDocument xmlDoc, string fileName) 
        {
            xmlDoc.Save($"../../Export/{fileName}"); // file name with extention

            Console.WriteLine(xmlDoc);
            Console.WriteLine($"\nResult saved to file: Export/{fileName}");
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

                // Create XML Document
                XDocument salesXmlDoc = new XDocument();
                XElement salesXml = new XElement("sales");

                foreach (var sale in sales)
                {
                    XElement carXml = new XElement("car",
                        new XAttribute("make", sale.Car.Make),
                        new XAttribute("model", sale.Car.Model),
                        new XAttribute("travelled-distance", sale.Car.TravelledDistance)
                        );
                    XElement customerXml = new XElement("customer-name", sale.CustomerName);
                    XElement discountXml = new XElement("discount", sale.Discount);
                    XElement priceXml = new XElement("price", sale.Price);
                    XElement priceWithDiscountXml = new XElement("price-with-discount", sale.PriceWithDiscount);

                    XElement saleXml = new XElement("sale");
                    saleXml.Add(carXml, customerXml, discountXml, priceXml, priceWithDiscountXml);

                    salesXml.Add(saleXml);
                }
                salesXmlDoc.Add(salesXml);

                // Export & Print
                ExportToFileAndPrint(salesXmlDoc, "sales-discounts.xml");
            }
        }

        // TODO 2- 5

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
                XDocument carsXmlDoc = new XDocument();
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
                carsXmlDoc.Add(carsXml);

                // Export & Print
                ExportToFileAndPrint(carsXmlDoc, "cars.xml");
            }
        }

        private static void SeedSales()
        {
            Console.WriteLine("Seeding Sales");
            using (CarDealerContext context = new CarDealerContext())
            {
                int[] discounts = { 0, 5, 10, 15, 20, 30, 40, 50 };
                int carsCount = context.Cars.Count();
                int customersCount = context.Customers.Count();
                int discountsCount = discounts.Count();
                int salesCount = 200;

                Random rnd = new Random();

                for (int i = 0; i < salesCount; i++)
                {
                    int customerId = rnd.Next(1, customersCount + 1);
                    int carId = rnd.Next(1, carsCount + 1);
                    int discount = discounts[rnd.Next(0, discountsCount)];

                    // Adding additional discount for young drivers
                    bool isYoungDriver = context.Customers.Find(customerId).IsYoungDriver;
                    if (isYoungDriver) discount += 5;

                    Sale newSale = new Sale()
                    {
                        CarId = carId,
                        CustomerId = customerId,
                        Discount = discount / 100m
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
            Console.WriteLine("Seeding Cars");
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
                    // Adding between 10 and 20 parts to a car
                    int carPartsCount = rnd.Next(10, 21);
                    for (int i = 0; i < carPartsCount; i++)
                    {
                        newCar.Parts.Add(context.Parts.Find(rnd.Next(1, partsCount + 1)));
                    }
                    context.Cars.Add(newCar);
                }
                context.SaveChanges();
            }
        }

        private static void SeedParts()
        {
            Console.WriteLine("Seeding Parts");
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
                        SupplierId = rnd.Next(1, suppliersCount + 1)
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
