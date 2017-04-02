namespace ProductsShop.Client
{
    using Data;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

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
                "2. Successfully Sold Products\n" +
                "3. Categories By Products Count\n" +
                "4. Users and Products\n" +
                "Enter option or [end]: ");

                string option = Console.ReadLine();
                bool isValidOption = true;
                switch (option)
                {
                    case "1": ProductsInRange(); break;
                    case "2": SuccessfullySoldProducts(); break;
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

        private static void UsersAndProducts()
        {
            Console.WriteLine("Solution to Query 4 - Users and Products");

            using (ProductsShopContext context = new ProductsShopContext())
            {
                var usersWithProducts = context.Users
                    //.Include("ProductsSold")
                    .Where(u => u.ProductsSold.Count() > 0)
                    .OrderByDescending(u => u.ProductsSold.Count())
                    .ThenBy(u => u.LastName)
                    .Select(u => new
                    {
                        FirstName = u.FirstName, // null not string.Empty
                        LastName = u.LastName,
                        Age = u.Age,
                        SoldProducts = new
                        {
                            Count = u.ProductsSold.Count(),
                            Products = u.ProductsSold
                                        .Select(p => new
                                        {
                                            Name = p.Name,
                                            Price = p.Price
                                        })
                        }
                    });
                var usersCountObj = new
                {
                    UsersCounts = usersWithProducts.Count(),
                    Users = usersWithProducts
                };
                string usersJson = JsonConvert.SerializeObject(usersCountObj, Formatting.Indented);
                Console.WriteLine(usersJson);
            }
        }

        private static void CategoriesByProductsCount()
        {
            Console.WriteLine("Solution to Query 3 - Categories By Products Count");

            using (ProductsShopContext context = new ProductsShopContext())
            {
                var categories = context.Categories
                    .OrderBy(c => c.Name)
                    .Select(c => new
                    {
                        Category = c.Name,
                        ProductsCount = c.Products.Count(),
                        AveragePrice = c.Products.Average(p => p.Price),
                        TotalRevenue = c.Products.Sum(p => p.Price)
                    });
                string categoriesJson = JsonConvert.SerializeObject(categories, Formatting.Indented);
                Console.WriteLine(categoriesJson);
            }
        }

        private static void SuccessfullySoldProducts()
        {
            Console.WriteLine("Solution to Query 2 - Successfully Sold Products");

            using (ProductsShopContext context = new ProductsShopContext())
            {
                var users = context.Users
                    //.Include("ProductsSold")
                    .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .Select(u => new
                    {
                        FirstName = u.FirstName ?? string.Empty,
                        LastName = u.LastName,
                        SoldProducts = u.ProductsSold
                                        .Where(p => p.BuyerId != null)
                                        .Select(p => new
                                        {
                                            Name = p.Name,
                                            Price = p.Price,
                                            BuyerFirstName = p.Buyer.FirstName ?? string.Empty,
                                            BuyerLastName = p.Buyer.LastName
                                        })
                    });
                string usersJson = JsonConvert.SerializeObject(users, Formatting.Indented);
                Console.WriteLine(usersJson);
            }
        }

        private static void ProductsInRange()
        {
            Console.WriteLine("Solution to Query 1 - Products In Range");

            using (ProductsShopContext context = new ProductsShopContext())
            {
                var products = context.Products
                    //.Include("Seller") // if collections/ props in models are not virtual 
                    .Where(p => p.Price >= 500 && p.Price <= 1000)
                    .OrderBy(p => p.Price)
                    .Select(p => new
                    {
                        Name = p.Name,
                        Price = p.Price,
                        Seller = p.Seller.FirstName + " " + p.Seller.LastName
                    });
                string productsJson = JsonConvert.SerializeObject(products, Formatting.Indented);
                Console.WriteLine(productsJson);
            }
        }

        private static void ImportCategories()
        {
            Console.WriteLine("Seeding Categories");

            using (ProductsShopContext context = new ProductsShopContext())
            {
                string json = File.ReadAllText("../../Import/categories.json");
                List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(json);
                context.Categories.AddRange(categories);
                context.SaveChanges();

                int index = 0;
                int categoriesCount = context.Categories.Count();
                var products = context.Products;

                foreach (Product p in products)
                {
                    p.Categories.Add(context.Categories.Find(++index * 13 % categoriesCount + 1));
                }
                context.SaveChanges();
            }
        }

        private static void ImportProducts()
        {
            Console.WriteLine("Seeding Products");

            using (ProductsShopContext context = new ProductsShopContext())
            {
                string json = File.ReadAllText("../../Import/products.json");
                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);

                int index = 0;
                int usersCount = context.Users.Count();

                foreach (Product product in products)
                {
                    product.SellerId = index++ % usersCount + 1;
                    if (index % 3 != 0)
                    {
                        product.BuyerId = index * 3 % usersCount + 1;
                    }
                }
                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }

        private static void ImportUsers()
        {
            Console.WriteLine("Seeding Users");

            using (ProductsShopContext context = new ProductsShopContext())
            {
                string json = File.ReadAllText("../../Import/users.json");
                List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }

        private static void SeedData()
        {
            ImportUsers();
            ImportProducts();
            ImportCategories();
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
