namespace Bakery
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BakeryContext : DbContext
    {
        public BakeryContext()
            : base("name=BakeryContext")
        {
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Distributor> Distributors { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasMany(e => e.Ingredients)
                .WithOptional(e => e.Country)
                .HasForeignKey(e => e.OriginCountryId);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.PhoneNumber)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Feedback>()
                .Property(e => e.Rate)
                .HasPrecision(4, 2);

            modelBuilder.Entity<Ingredient>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Ingredients)
                .Map(m => m.ToTable("ProductsIngredients").MapLeftKey("IngredientId").MapRightKey("ProductId"));

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);
        }
    }
}
