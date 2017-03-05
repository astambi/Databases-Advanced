using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery
{
    class StartupBakery
    {
        static void Main(string[] args)
        {
            /* EF Solution to Problem 20.Products by One Distributor 
             * Feb.19, 2017 Exam (Databases Basics - MS SQL Server) */
            BakeryContext context = new BakeryContext();
            var singleDistProducts = context.Products
                .Where(ProductHasSingleDistributor)
                .OrderBy(p => p.Id);
            PrintProducts(singleDistProducts);
        }

        private static void PrintProducts(IOrderedEnumerable<Product> products)
        {
            foreach (Product prod in products)
            {
                decimal? avgRate = prod.Feedbacks.Select(x => x.Rate).Average();
                Distributor dist = prod.Ingredients.First().Distributor;
                //if (avgRate != null) // uncomment to show rated products only (no null ratings)
                Console.WriteLine($"{prod.Name,-20} | {avgRate,9:f6} | {dist.Name,-20} | {dist.Country.Name}");
            }
        }

        private static bool ProductHasSingleDistributor(Product product)
        {
            Ingredient ingredient = product.Ingredients.FirstOrDefault();
            if (ingredient == null) { return false; }
            int? distributorId = ingredient.DistributorId;
            return product.Ingredients.All(i => i.DistributorId == distributorId);
        }
    }
}