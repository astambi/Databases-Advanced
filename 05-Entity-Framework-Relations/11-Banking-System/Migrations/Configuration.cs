namespace _11_Banking_System.Migrations
{
    using Models;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BankingSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BankingSystemContext context)
        {
            //  This method will be called after migrating to the latest version.
            Console.WriteLine("Seeding data");

            // See Users: Problem 12
            context.Users.AddOrUpdate(u => u.Username,
                new User()
                {
                    Username = "testUser123",
                    Password = "aaaBBB123456789",
                    Email = "test.user@gmail.com"
                });
            context.SaveChanges();

            // Seed SavingAccounts: Problem 11
            context.SavingAccounts.AddOrUpdate(a => a.AccountNumber,
                new SavingAccount()
                {
                    AccountNumber = "BG12BUIN12",
                    Balance = 1000m,
                    InterestRate = 0.1m,
                    UserId = 1 // Problem 12
                });
            context.SaveChanges();

            // Seed CheckingAccounts: Problem 11
            context.CheckingAccounts.AddOrUpdate(a => a.AccountNumber,
                new CheckingAccount()
                {
                    AccountNumber = "BG89UBBS12",
                    Balance = 1000m,
                    Fee = 1.50m,
                    UserId = 1 // Problem 12
                });
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
