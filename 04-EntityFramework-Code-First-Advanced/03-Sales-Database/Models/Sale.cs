using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Sales_Database.Models
{
    public class Sale
    {
        public Sale()
        {
        }

        public int Id { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }
        public StoreLocation StoreLocation { get; set; }
        public DateTime Date { get; set; }
    }
}
