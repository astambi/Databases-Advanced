namespace Demo
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class DemoStartup
    {
        static void Main(string[] args)
        {
            // Install AutoMapper from NuGet

            Console.WriteLine("Initializing database");
            using (var context = new QueryContext())
            {
                context.Database.Initialize(true);

                ManualMappingDTO(context);
                AutoMappingDTO(context);
                AutoMappingWithConfigurationDTO(context);
                AutoMappingCollection(context);
                MappingProperties(context);
            }
        }

        private static void MappingProperties(QueryContext context)
        {
            MapperConfig.Init();

            Order order = context.Orders.Find(1);
            OrderDTO orderDTO = Mapper.Map<OrderDTO>(order);

            Console.WriteLine($"Client {orderDTO.ClientName} - Order Total {orderDTO.OrderTotal}");
        }

        private static void AutoMappingCollection(QueryContext context)
        {
            MapperConfig.Init(); // custom configuration
            // v.1
            //Product[] products = context.Products.ToArray();
            //List<ProductDTO> productDTOs = Mapper.Map<Product[], List<ProductDTO>>(products);

            // v.2
            List<ProductDTO> productDTOs = context.Products
                .ProjectTo<ProductDTO>()
                .ToList();

            foreach (var productDTO in productDTOs)
            {
                Console.WriteLine($"{productDTO.Name} - Cost {productDTO.Cost} - Qty {productDTO.StockQuantity}");
            }
        }

        private static void AutoMappingWithConfigurationDTO(QueryContext context)
        {
            MapperConfig.Init(); // custom configuration

            Product product = context.Products.Find(2);
            ProductDTO productDTO = Mapper.Map<ProductDTO>(product);

            Console.WriteLine($"{productDTO.Name} - Cost {productDTO.Cost} - Qty {productDTO.StockQuantity}");
        }

        private static void AutoMappingDTO(QueryContext context)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductCostDTO>());

            Product product = context.Products.Find(2);
            ProductCostDTO productDTO = Mapper.Map<ProductCostDTO>(product);

            Console.WriteLine($"{productDTO.Name} - Cost {productDTO.Cost}");
        }

        private static void ManualMappingDTO(QueryContext context)
        {
            Product product = context.Products.Find(2);
            ProductQtyDTO productDTO = MapToDTO(product);

            Console.WriteLine($"{productDTO.Name} - Qty {productDTO.StockQuantity}");
        }

        private static ProductQtyDTO MapToDTO(Product product) // manual mapping
        {
            var productDTO = new ProductQtyDTO()
            {
                Name = product.Name,
                StockQuantity = product.ProductStocks.Sum(ps => ps.Quantity)
            };
            return productDTO;
        }

        public class ProductCostDTO
        {
            public string Name { get; set; }
            public decimal Cost { get; set; }
        }

        public class ProductQtyDTO
        {
            public string Name { get; set; }
            public int StockQuantity { get; set; }
        }

        public class ProductDTO
        {
            public string Name { get; set; }
            public decimal Cost { get; set; }
            public int StockQuantity { get; set; }
        }

        public class OrderDTO
        {
            public string ClientName { get; set; }
            public decimal OrderTotal { get; set; }
        }
    }
}
