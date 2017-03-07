using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Local_Store
{
    class P01_LocalStore
    {
        static void Main(string[] args)
        {
            ProductsContext context = new ProductsContext();
            Console.WriteLine("Creating Database [Products.CodeFirst] and seeding data");

            Product apple = new Product("Apple", "Kaufland", "Fruit", 2.2m) { };
            Product banana = new Product("Banana", "Kaufland", "Fruit", 3.3m);
            Product tomato = new Product("Red Tomato", "Kaufland", "Vegetable", 2.5m);
            Product carrot = new Product("Carrot", "Kaufland", "Vegetable", 2.8m);
            Product millet = new Product("Millet", "Zelen Bio", "Cereal", 3.3m);
            context.Products.AddRange(new[] { apple, banana, tomato, carrot, millet });

            context.SaveChanges();
        }
    }
}
