namespace _11_Banking_System
{
    using Migrations;
    using Models;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Annotations;

    public class BankingSystemContext : DbContext
    {
        public BankingSystemContext()
            : base("name=BankingSystemContext")
        {
            Database.SetInitializer<BankingSystemContext>(new MigrateDatabaseToLatestVersion<BankingSystemContext, Configuration>());
        }

        public virtual DbSet<CheckingAccount> CheckingAccounts { get; set; }

        public virtual DbSet<SavingAccount> SavingAccounts { get; set; }

        public virtual DbSet<User> Users { get; set; } // Problem 12

        protected override void OnModelCreating(DbModelBuilder modelBuilder) // Problem 12
        {
            // Checking Account
            // Primary key
            modelBuilder.Entity<CheckingAccount>()
                        .HasKey(a => a.Id);
            // Required 
            modelBuilder.Entity<CheckingAccount>()
                        .Property(a => a.AccountNumber)
                        .IsRequired();
            modelBuilder.Entity<CheckingAccount>()
                        .HasRequired(a => a.User)
                        .WithMany(u => u.CheckingAccounts);
            // Unique
            modelBuilder.Entity<CheckingAccount>()
                        .Property(a => a.AccountNumber)
                        .HasColumnAnnotation("IX_SavingAccounts_AccountNumber", 
                        new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            // Saving Account
            // Primary key
            modelBuilder.Entity<SavingAccount>()
                        .HasKey(a => a.Id);
            // Required
            modelBuilder.Entity<SavingAccount>()
                        .Property(a => a.AccountNumber)
                        .IsRequired();
            modelBuilder.Entity<SavingAccount>()
                        .HasRequired(a => a.User)
                        .WithMany(u => u.SavingAccounts);
            // Unique
            modelBuilder.Entity<SavingAccount>()
                        .Property(a => a.AccountNumber)
                        .HasColumnAnnotation("IX_SavingAccounts_Username", 
                        new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            // User
            // Primary key
            modelBuilder.Entity<User>()
                        .HasKey(u => u.Id);
            // Required
            modelBuilder.Entity<User>()
                        .Property(u => u.Username)
                        .IsRequired();
            modelBuilder.Entity<User>()
                        .Property(u => u.Email)
                        .IsRequired();
            modelBuilder.Entity<User>()
                        .Property(u => u.Password)
                        .IsRequired();
            // Unique
            modelBuilder.Entity<User>()
                        .Property(u => u.Username)
                        .HasColumnAnnotation("IX_Users_Username", 
                        new IndexAnnotation(new IndexAttribute { IsUnique = true }));
            modelBuilder.Entity<User>()
                        .Property(u => u.Email)
                        .HasColumnAnnotation("IX_Users_Email", 
                        new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            base.OnModelCreating(modelBuilder);
        }

    }
}