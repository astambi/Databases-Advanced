namespace Demo
{
    using Models.Dtos;
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    class DemoStartup
    {
        static void Main(string[] args)
        {
            ParsingXML();
            LoadingXML();
            UsingXMLWithLINQ();
            CreatingSavingXML();
            XMLSerializer();            
        }

        private static void XMLSerializer()
        {
            Console.WriteLine("\nXML Serializer:");
            XmlSerializer serializer = new XmlSerializer(typeof(ProductDto));

            // Serialize 
            using (StreamWriter writer = new StreamWriter("../../Export/productDtoSerialized.xml"))
            {
                ProductDto product = new ProductDto { Name = "Piano", Cost = 1500m, StockQuantity = 3 };
                serializer.Serialize(writer, product);
            }

            // Deserialize 
            using (StreamReader reader = new StreamReader("../../Export/productDtoSerialized.xml"))
            {
                ProductDto productDto = (ProductDto)serializer.Deserialize(reader);
                Console.WriteLine(productDto);
            }
        }

        private static void CreatingSavingXML()
        {
            Console.WriteLine("\nCreating XML:");
            // Create XML
            XDocument xmlDoc = new XDocument();
            xmlDoc.Add(
              new XElement("books",
                new XElement("book",
                  new XElement("author", "Don Box"),
                  new XElement("title", "ASP.NET", new XAttribute("lang", "en"))
            )));
            Console.WriteLine(xmlDoc);

            // Save XML to file
            xmlDoc.Save("../../Export/books.xml");
            xmlDoc.Save("../../Export/booksNoFormatting.xml", SaveOptions.DisableFormatting);
        }

        private static void UsingXMLWithLINQ()
        {
            Console.WriteLine("\nUsing XML: Toyota models");
            XDocument xmlDoc = XDocument.Load("../../Import/cars.xml");
            xmlDoc.Root.Elements()
                .Where(e => e.Element("make").Value == "Toyota" &&
                            long.Parse(e.Element("travelled-distance").Value) >= 300000L)
                .Select(c => new
                {
                    Model = c.Element("model").Value,
                    Distance = long.Parse(c.Element("travelled-distance").Value)
                })
                .OrderByDescending(c => c.Distance)
                .ThenBy(c => c.Model)
                .ToList()
                .ForEach(m => Console.WriteLine($"{m.Model} = {m.Distance}"));

            Console.WriteLine("\nUsing XML: Young drivers");
            XDocument xmlDocCustomers = XDocument.Load("../../Import/customers.xml");
            xmlDocCustomers.Root.Elements()
                .Where(e => e.Element("is-young-driver").Value == "true")
                .Select(c => c.Attribute("name").Value)
                .OrderBy(d => d)
                .ToList()
                .ForEach(d => Console.WriteLine(d));
        }

        private static void LoadingXML()
        {
            Console.WriteLine("\nLoading XML:");
            XDocument xmlDocCars = XDocument.Load("../../Import/cars.xml");
            var cars = xmlDocCars.Root.Elements()
                .Take(10);
            foreach (XElement car in cars)
            {
                string make = car.Element("make").Value;
                string model = car.Element("model").Value;
                string distance = car.Element("travelled-distance").Value;
                Console.WriteLine($"{make} {model} - {distance}");
            }

            Console.WriteLine("\nLoading XML:");
            XDocument xmlDocCustomers = XDocument.Load("../../Import/customers.xml");
            var customers = xmlDocCustomers.Root.Elements()
                .Take(10);
            foreach (XElement customer in customers)
            {
                string name = customer.Attribute("name").Value;
                string birthDate = customer.Element("birth-date").Value;
                string isYoungDriver = customer.Element("is-young-driver").Value;
                Console.WriteLine($"{name} - {isYoungDriver} - {birthDate}");
            }
        }

        private static void ParsingXML()
        {
            Console.WriteLine("Parsing XML:");
            string str = 
                @"<?xml version=""1.0""?>
                <!-- comment at the root level -->
                <Root>
                    <Child>Content</Child>
                </Root>";
            XDocument xmlDoc = XDocument.Parse(str);
            Console.WriteLine(xmlDoc);
        }
    }
}
