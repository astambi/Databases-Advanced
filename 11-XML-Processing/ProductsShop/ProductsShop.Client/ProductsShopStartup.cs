namespace ProductsShop.Client
{
    using Data;
    using Models;
    using System;
    using System.Linq;
    using System.Xml.Linq;

    class ProductsShopStartup
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
                Console.Write("Solutions to ProductsShop:\n" +
                "1. Products In Range\n" +
                "2. Sold Products\n" +
                "3. Categories By Products Count\n" +
                "4. Users and Products\n" +
                "Enter option or [end]: ");

                string option = Console.ReadLine();
                bool isValidOption = true;
                switch (option)
                {
                    case "1": ProductsInRange(); break;
                    case "2": SoldProducts(); break;
                    case "3": CategoriesByProductsCount(); break;
                    case "4": UsersAndProducts(); break;
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

        private static void UsersAndProducts()
        {
            Console.WriteLine("Solution to Query 4 - Users and Products");
            using (ProductsShopContext context = new ProductsShopContext())
            {
                // Users with Sold Products
                var usersWithProducts = context.Users
                    .Where(u => u.ProductsSold.Any())
                    .OrderByDescending(u => u.ProductsSold.Count())
                    .ThenBy(u => u.LastName)
                    .Select(u => new
                    {
                        FirstName = u.FirstName,    // optional
                        LastName = u.LastName,
                        Age = u.Age,                // optional
                        SoldProducts = u.ProductsSold.Select(p => new
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                    });

                // Create XML Document
                XDocument xmlDoc = new XDocument();
                XElement usersXml = new XElement("users",
                    new XAttribute("count", usersWithProducts.Count()));

                foreach (var user in usersWithProducts)
                {
                    // User
                    XElement userXml = new XElement("user");
                    if (user.FirstName != null)
                    {
                        userXml.Add(new XAttribute("first-name", user.FirstName));  // no attribute if null
                    }
                    userXml.Add(new XAttribute("last-name", user.LastName));        // required
                    if (user.Age != null)
                    {
                        userXml.Add(new XAttribute("age", user.Age));               // no attribute if null
                    }

                    // Sold Products
                    XElement soldProductsXml = new XElement("sold-products",
                        new XAttribute("count", user.SoldProducts.Count()));
                    foreach (var product in user.SoldProducts)
                    {
                        XElement productXml = new XElement("product",
                            new XAttribute("name", product.Name),
                            new XAttribute("price", product.Price));
                        soldProductsXml.Add(productXml);
                    }
                    // add sold products to user
                    userXml.Add(soldProductsXml);
                    // Add user to users
                    usersXml.Add(userXml);
                }
                xmlDoc.Add(usersXml);

                // Export & Print
                ExportAndPrint(xmlDoc, "users-and-products");
            }
        }

        private static void CategoriesByProductsCount()
        {
            Console.WriteLine("Solution to Query 3 - Categories By Products Count");
            using (ProductsShopContext context = new ProductsShopContext())
            {
                // Categories by ProductsCount
                var categories = context.Categories
                    .Select(c => new
                    {
                        Name = c.Name,
                        ProductsCount = c.Products.Count(),
                        AveragePrice = c.Products.Average(p => p.Price),
                        TotalRevenue = c.Products.Sum(p => p.Price)
                    })
                    .OrderByDescending(c => c.ProductsCount); // DESC in example provided

                // Create XML Document
                XDocument xmlDoc = new XDocument();
                XElement categoriesXml = new XElement("categories");

                foreach (var category in categories)
                {
                    XElement categoryXml = new XElement("category",
                        new XAttribute("name", category.Name),
                        new XElement("products-count", category.ProductsCount),
                        new XElement("average-price", category.AveragePrice),
                        new XElement("total-revenue", category.TotalRevenue));
                    categoriesXml.Add(categoryXml);
                }
                xmlDoc.Add(categoriesXml);

                // Export & Print
                ExportAndPrint(xmlDoc, "categories-by-products");
            }
        }

        private static void SoldProducts()
        {
            Console.WriteLine("Solution to Query 2 - Successfully Sold Products");
            using (ProductsShopContext context = new ProductsShopContext())
            {
                // Users & sold products
                var users = context.Users
                    .Where(u => u.ProductsSold.Any())
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .Select(u => new
                    {
                        FirstName = u.FirstName, // optional
                        LastName = u.LastName,
                        SoldProducts = u.ProductsSold.Select(p => new
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                    });

                // Create XML Document
                XDocument xmlDoc = new XDocument();
                XElement usersXml = new XElement("users");

                foreach (var user in users)
                {
                    // User
                    XElement userXml = new XElement("user");
                    if (user.FirstName != null)
                    {
                        userXml.Add(new XAttribute("first-name", user.FirstName));  // no attribute if null
                    }
                    userXml.Add(new XAttribute("last-name", user.LastName));        // required

                    // Sold products
                    XElement soldProductsXml = new XElement("sold-products");
                    foreach (var product in user.SoldProducts)
                    {
                        XElement productXml = new XElement("product",
                            new XElement("name", product.Name),
                            new XElement("price", product.Price));
                        soldProductsXml.Add(productXml);
                    }
                    // Add sold products to user
                    userXml.Add(soldProductsXml);

                    // Add user to users
                    usersXml.Add(userXml);
                }
                xmlDoc.Add(usersXml);

                // Exmport & Print
                ExportAndPrint(xmlDoc, "users-sold-products");
            }
        }

        private static void ProductsInRange()
        {
            Console.WriteLine("Solution to Query 1 - Products In Range");
            using (ProductsShopContext context = new ProductsShopContext())
            {
                // Products in range
                var products = context.Products
                    .Where(p => p.Price >= 1000 &&
                                p.Price <= 2000 &&
                                p.BuyerId != null)
                    .OrderBy(p => p.Price)
                    .Select(p => new
                    {
                        Name = p.Name,
                        Price = p.Price,
                        Buyer = (p.Buyer.FirstName + " " + p.Buyer.LastName).Trim()
                    });

                // Create XML Document
                XDocument xmlDoc = new XDocument();
                XElement productsXml = new XElement("products");

                foreach (var product in products)
                {
                    XElement productXml = new XElement("product",
                        new XAttribute("name", product.Name),
                        new XAttribute("price", product.Price),
                        new XAttribute("buyer", product.Buyer));
                    productsXml.Add(productXml);
                }
                xmlDoc.Add(productsXml);

                // Export & Print
                ExportAndPrint(xmlDoc, "products-in-range");
            }
        }

        private static void SeedCategories()
        {
            Console.WriteLine("Seeding Categories (assigning random categories to products)");
            using (ProductsShopContext context = new ProductsShopContext())
            {
                // Add Categories
                XDocument xmlDoc = XDocument.Load("../../Import/categories.xml");
                var categories = xmlDoc.Root.Elements();

                foreach (XElement category in categories)
                {
                    string name = category.Element("name").Value; // required
                    Category newCategory = new Category()
                    {
                        Name = name
                    };
                    context.Categories.Add(newCategory);
                }
                context.SaveChanges();

                // Add random categories to Products
                int categoriesCount = context.Categories.Count();
                var products = context.Products;
                Random rnd = new Random();

                // For each product generate between 1 and 3 random categories
                foreach (Product product in products)
                {
                    int productCategoriesCount = rnd.Next(1, 4);                // random number of categories [1, 3]
                    for (int i = 0; i < productCategoriesCount; i++)
                    {
                        int categoryIndex = rnd.Next(1, categoriesCount + 1);   // random category
                        product.Categories.Add(context.Categories.Find(categoryIndex));
                    }
                }
                context.SaveChanges();
            }
        }

        private static void SeedProducts()
        {
            Console.WriteLine("Seeding Products (with random seller & buyer, null buyer for some products)");
            using (ProductsShopContext context = new ProductsShopContext())
            {
                XDocument xmlDoc = XDocument.Load("../../Import/products.xml");
                var products = xmlDoc.Root.Elements();
                int usersCount = context.Users.Count();
                Random rnd = new Random();

                foreach (XElement product in products)
                {
                    string name = product.Element("name").Value;
                    decimal price = decimal.Parse(product.Element("price").Value);

                    int sellerId = rnd.Next(1, usersCount + 1);                 // random seller
                    int? buyerId = rnd.Next(-usersCount / 3, usersCount + 1);   // random buyer
                    if (buyerId < 1) buyerId = null;                            // leaving some products without buyer

                    Product newProduct = new Product()
                    {
                        Name = name,
                        Price = price,
                        SellerId = sellerId,    // random seller
                        BuyerId = buyerId       // random buyer (leaving some products without a buyer)
                    };
                    context.Products.Add(newProduct);
                }
                context.SaveChanges();
            }
        }

        private static void SeedUsers()
        {
            Console.WriteLine("Seeding Users");
            using (ProductsShopContext context = new ProductsShopContext())
            {
                XDocument xmlDoc = XDocument.Load("../../Import/users.xml");
                var users = xmlDoc.Root.Elements();

                foreach (XElement user in users)
                {
                    string lastName = user.Attribute("last-name").Value; // required
                    string firstName = null;                             // optional 
                    int? age = null;                                     // optional 

                    XAttribute firstNameAttr = user.Attribute("first-name");
                    if (firstNameAttr != null) firstName = firstNameAttr.Value;

                    XAttribute ageAttr = user.Attribute("age");
                    if (ageAttr != null) age = int.Parse(ageAttr.Value);

                    User newUser = new User()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Age = age
                    };
                    context.Users.Add(newUser);
                }
                context.SaveChanges();
            }
        }

        private static void SeedData()
        {
            SeedUsers();
            SeedProducts();
            SeedCategories();
            Console.WriteLine();
        }

        private static void InitializeDatabase()
        {
            using (ProductsShopContext context = new ProductsShopContext())
            {
                Console.WriteLine("Initializing database [ProductsShop]");
                context.Database.Initialize(true);
            }
        }
    }
}
