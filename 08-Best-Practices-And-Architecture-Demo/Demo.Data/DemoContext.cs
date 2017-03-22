namespace Demo.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DemoContext : DbContext
    {
        public DemoContext()
            : base("name=DemoContext")
        {
        }

        public virtual DbSet<Person> Persons { get; set; }


    }
}