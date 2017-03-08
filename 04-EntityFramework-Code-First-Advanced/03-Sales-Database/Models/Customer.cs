using System.Collections.Generic;

namespace _03_Sales_Database.Models
{
    public class Customer
    {
        public Customer()
        {
            this.SalesForCustomer = new HashSet<Sale>();
        }

        public int Id { get; set; }
        //public string Name { get; set; }      // Remove for Problem 6
        public string FirstName { get; set; }   // Add for Problem 6
        public string LastName { get; set; }    // Add for Problem 6
        public int Age { get; set; }            // Add for Problem 7
        public string Email { get; set; }
        public string CreditCardNumber { get; set; }
        public virtual ICollection<Sale> SalesForCustomer { get; set; }
    }
}
