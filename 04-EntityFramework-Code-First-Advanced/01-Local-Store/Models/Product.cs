using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Local_Store
{
    public class Product
    {
        public Product(string name, string distributor, string description, decimal price)
        {
            this.Name = name;
            this.Distributor = distributor;
            this.Description = description;
            this.Price = price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Distributor { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
