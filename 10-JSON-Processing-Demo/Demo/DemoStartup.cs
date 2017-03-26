namespace Demo
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models;
    using Models.Dtos;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Script.Serialization;

    class DemoStartup
    {
        static void Main(string[] args)
        {
            // Add Reference to System.Web.Extensions (JSSerializer)
            // Install Newtonsoft.Json

            MapperConfig.Init();

            // JavaScriptSerializer
            SerializeDeserialize();
            Serializing();
            Deserializing();
            UsingSerializerWithDictionaries();
            UsingSerializerWithDTOs();

            // JSON.Net
            UsingJsonNet();
            UsingJsonNetWithAnonymousTypes();
            UsingJsonNetWithAttributes();
            UsingJsonNetWithLINQToJson();
            UsingJsonNetAttributesToPreventCircularRef();
        }

        private static void UsingJsonNetAttributesToPreventCircularRef()
        {
            using (QueryContext context = new QueryContext())
            {
                /* Use the attribute [JsonIgnore] on virtual collections in Models 
                 * to prevent circular references with Json serialization 
                 */

                var products = context.Products;
                string jsonProducts = JsonConvert.SerializeObject(products, Formatting.Indented);
                Console.WriteLine(jsonProducts);

                var ordersProducts = context.OrderProduts;
                string jsonOrdersProducts = JsonConvert.SerializeObject(ordersProducts, Formatting.Indented);
                Console.WriteLine(jsonOrdersProducts);
            }
        }

        private static void UsingJsonNetWithLINQToJson()
        {
            using (QueryContext context = new QueryContext())
            {
                ProductDTO product = Mapper.Map<ProductDTO>(context.Products.FirstOrDefault());
                string jsonProduct = JsonConvert.SerializeObject(product);
                JObject jObj = JObject.Parse(jsonProduct);

                Console.WriteLine($"JObject:\n{jObj}");
                Console.WriteLine($"JObject Name:  {jObj["Name"]}");
                Console.WriteLine($"JObject Price: {jObj["Price"]}");

                string json = @"{ 
                    'products': 
                        [
                            {'name': 'Fruits', 'products': ['apple', 'banana', 'orange']},
                            {'name': 'Vegetables', 'products': ['cucumber', 'potato', 'eggplant']}
                        ]}";
                JObject jsonObj = JObject.Parse(json);

                int index = 1;
                List<string> products = jsonObj["products"]
                    .Select(c => string.Format("{0}. {1} ({2})",
                                index++, c["name"], string.Join(", ", c["products"])))
                    .ToList();

                foreach (string item in products)
                {
                    Console.WriteLine(item);
                }
            }
        }

        private static void UsingJsonNetWithAttributes()
        {
            using (QueryContext context = new QueryContext())
            {
                // Using [JsonProperty("Price")] attribute for [Cost] in ProductDTO

                ProductDTO product = Mapper.Map<ProductDTO>(context.Products.FirstOrDefault());
                string jsonProduct = JsonConvert.SerializeObject(product, Formatting.Indented);
                Console.WriteLine(jsonProduct); // Cost => Price

                ProductDTO product2 = JsonConvert.DeserializeObject<ProductDTO>(jsonProduct);
                Console.WriteLine(product2);

                string jsonCostTest = @"{
                    'Name': 'Oil Pump',
                    'Cost': 1000.00,
                    'StockQuantity': 600}"; // using Price attribute for Cost prop
                ProductDTO costTest = JsonConvert.DeserializeObject<ProductDTO>(jsonCostTest);
                Console.WriteLine(costTest); // [Cost] is not recognized & not parsed => Cost = 0
            }
        }

        private static void UsingJsonNetWithAnonymousTypes()
        {
            string json = @"{ 
                'firstName': 'Vladimir',
                'lastName': 'Georgiev',
                'jobTitle': 'Technical Trainer' }";
            var template = new
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                JobTitle = string.Empty
            };
            var person = JsonConvert.DeserializeAnonymousType(json, template);
            Console.WriteLine($"JSON to anonymous objects:\n{person}");
        }

        private static void UsingJsonNet()
        {
            using (QueryContext context = new QueryContext())
            {
                Console.WriteLine("\nJSON.Net");

                // Product
                ProductDTO product = Mapper.Map<ProductDTO>(context.Products.FirstOrDefault());
                string jsonProduct = JsonConvert.SerializeObject(product, Formatting.Indented);
                Console.WriteLine($"\nJSON Product:\n{jsonProduct}");

                ProductDTO product2 = JsonConvert.DeserializeObject<ProductDTO>(jsonProduct);
                Console.WriteLine($"\nJSON to Product:\n{product2}");

                // Collection
                var products = context.Products.ProjectTo<ProductDTO>();
                string jsonProducts = JsonConvert.SerializeObject(products, Formatting.Indented);
                Console.WriteLine($"\nJSON Collection:\n{jsonProducts}");

                // Collection ForEach
                Console.WriteLine("\nJSON Collection (foreach):");
                foreach (ProductDTO p in products)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(p, Formatting.Indented));
                }
                Console.WriteLine("\nJSON to Collection (foreach):");
                var productsObj = JsonConvert.DeserializeObject<List<ProductDTO>>(jsonProducts);
                foreach (var obj in productsObj)
                {
                    Console.WriteLine(obj);
                }
            }
        }

        private static void UsingSerializerWithDTOs()
        {
            using (QueryContext context = new QueryContext())
            {
                Console.WriteLine("Initializing database");
                context.Database.Initialize(true);
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                Console.WriteLine("\nJavaScriptSerializer");

                // Product to JSON
                Product product = context.Products.FirstOrDefault();
                ProductDTO productDto = Mapper.Map<ProductDTO>(product);
                string jsonProduct = serializer.Serialize(productDto);
                Console.WriteLine($"\nJSON Product: {jsonProduct}");

                // JSON to Product
                var productDto2 = serializer.Deserialize<ProductDTO>(jsonProduct);
                Console.WriteLine($"JSON to ProductDto: {productDto2}");

                // Order to JSON
                Order order = context.Orders.FirstOrDefault();
                OrderDTO orderDto = Mapper.Map<OrderDTO>(order);
                string jsonOrder = serializer.Serialize(orderDto);
                Console.WriteLine($"\nJSON Order: {jsonOrder}");

                // JSON to Order
                var orderDto2 = serializer.Deserialize<OrderDTO>(jsonOrder);
                Console.WriteLine($"JSON to OrderDto: {orderDto2}");

                // Collection: JSON Serialize
                var productDtos = context.Products.ProjectTo<ProductDTO>();
                string jsonProductDtos = serializer.Serialize(productDtos);
                Console.WriteLine($"\nJSON Collection:\n{jsonProductDtos}");

                // JSON Collection Foreach
                Console.WriteLine("\nJSON Collection (foreach):");
                foreach (var p in productDtos)
                {
                    Console.WriteLine(serializer.Serialize(p));
                }

                // JSON to Collection
                var productsObj = serializer.Deserialize<List<ProductDTO>>(jsonProductDtos);
                Console.WriteLine("\nJSON to Collection (foreach):");
                foreach (var obj in productsObj)
                {
                    Console.WriteLine(obj);
                }

                // Dictionary: JSON Serialize
                Dictionary<string, ProductDTO> dictProductDtos = new Dictionary<string, ProductDTO>();
                foreach (ProductDTO pDto in productDtos)
                {
                    dictProductDtos.Add(pDto.Name, pDto);
                }
                string jsonDictProductDtos = serializer.Serialize(dictProductDtos);
                Console.WriteLine($"\nJSON Dictionary:\n{jsonDictProductDtos}");

                // JSON to Dictionary
                var dictObj = serializer.Deserialize<Dictionary<string, ProductDTO>>(jsonDictProductDtos);
                Console.WriteLine("\nJSON to Dictionary (foreach):");
                foreach (var obj in dictObj)
                {
                    Console.WriteLine(obj);
                }
            }
        }

        private static void UsingSerializerWithDictionaries()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            Person person1 = new Person
            {
                Name = "Parsifal",
                Age = 24
            };
            Person person2 = new Person
            {
                Name = "Amfortas",
                Age = 44
            };
            Dictionary<string, Person> knights = new Dictionary<string, Person>
            {
                { "Knight1", person1 },
                { "Knight2", person2 }
            };
            string jsonKnights = serializer.Serialize(knights);
            Console.WriteLine(jsonKnights);

            Dictionary<string, Person> objKnights = serializer.Deserialize<Dictionary<string, Person>>(jsonKnights);
            foreach (var pair in objKnights)
            {
                Console.WriteLine($"{pair.Key} {pair.Value}");
            }
        }

        private static void Deserializing()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string jsonP = "{'Name': 'John', 'Age':30, 'Dog':{'Name':'Swift'}}";
            Person person = serializer.Deserialize<Person>(jsonP);
            Console.WriteLine(person);

            string jsonD = "{'Name': 'Swift', 'Age':4, 'Owner':{'Name': 'John'}}";
            Dog dog = serializer.Deserialize<Dog>(jsonD);
            Console.WriteLine(dog);
        }

        private static void Serializing()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            Dog dog = new Dog
            {
                Name = "Sharo",
                Age = 6
            };
            Person person = new Person
            {
                Name = "Atanas",
                Age = 24,
                Dog = dog
            };
            //dog.Owner = person; // circular reference
            string jsonDog = serializer.Serialize(dog);
            string jsonPerson = serializer.Serialize(person);

            Console.WriteLine(jsonDog);
            Console.WriteLine(jsonPerson);
        }

        private static void SerializeDeserialize()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            Product product = new Product()
            {
                Name = "Book",
                Cost = 10.00m
            };
            string jsonProduct = serializer.Serialize(product);
            Console.WriteLine(jsonProduct);

            Product objProduct = serializer.Deserialize<Product>(jsonProduct);
            Console.WriteLine($"{objProduct.Name} {objProduct.Cost:f2}");
        }
    }
}
