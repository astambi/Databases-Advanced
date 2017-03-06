namespace _01_Local_Store
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ProductsContext : DbContext
    {
        public ProductsContext()
            : base("name=ProductsContext")
        {
        }

        public virtual DbSet<Product> Products { get; set; }
    }    
}