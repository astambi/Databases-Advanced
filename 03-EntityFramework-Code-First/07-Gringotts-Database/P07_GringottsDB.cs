using _07_Gringotts_Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07_Gringotts_Database
{
    class P07_GringottsDB
    {
        static void Main(string[] args)
        {
            GringottsContext context = new GringottsContext();
            context.Database.Initialize(true);
            SeedData(context);
        }

        private static void SeedData(GringottsContext context)
        {
            // constructor with required LastName & Age only
            context.WizardDeposits.Add(new WizardDeposit("Nakov", 36));
            context.WizardDeposits.Add(new WizardDeposit("Geogiev", 35));

            // empty constructor & trimming empty spaces from string input
            context.WizardDeposits.Add(new WizardDeposit() 
            {
                FirstName = "    Albus    ",
                LastName = "   Dumbledore    ",
                Age = 150,
                MagicWandCreator = "   Antioch Peverell   ",
                DepositGroup = "   Wizards Group   ", 
                MagicWandSize = 15,
                DepositStartDate = new DateTime(2016, 10, 20),
                DepositExpirationDate = new DateTime(2020, 10, 20),
                DepositAmount = 2000.24m,
                DepositCharge = 0.2,
                IsDepositExpired = false
            });            

            // combined constructor with required LastName & Age + misc
            context.WizardDeposits.Add(new WizardDeposit("Kennedy", 60) 
            {
                FirstName = "Nigel",
                Notes = "A brilliant violinist",
                MagicWandCreator = "Guarneri",
                MagicWandSize = 11,
                DepositStartDate = new DateTime(2017, 1, 11),
                DepositExpirationDate = new DateTime(2019, 12, 11),
                DepositAmount = 100000.00m,
                DepositInterest = 0.055m,
                DepositCharge = 0.2,
                IsDepositExpired = false
            });
            context.WizardDeposits.Add(new WizardDeposit("Eschkenazy", 46)
            {
                FirstName = "Vesko",
                Notes = "Royal Concertgebouw Orchestra's concertmaster",
                MagicWandCreator = "Guarneri del Gesu",
                MagicWandSize = 10,
                DepositStartDate = new DateTime(2017, 3, 2),
                DepositExpirationDate = new DateTime(2017, 10, 2),
                DepositAmount = 50000m,
                DepositInterest = 0.050m,
                DepositCharge = 0.2,
                IsDepositExpired = false
            });
            context.WizardDeposits.Add(new WizardDeposit("Bozhanov", 32)
            {
                FirstName = "Evgeni",
                Notes = "He can produce more nuances of tone in a measure of music than most pianists find in a lifetime",
                MagicWandSize = 10,
                DepositStartDate = new DateTime(2016, 12, 5),
                DepositExpirationDate = new DateTime(2017, 2, 5),
                DepositAmount = 7500m,
                DepositInterest = 0.047m,
                DepositCharge = 0.2,
                IsDepositExpired = true
            });
            
            context.SaveChanges();
        }
    }
}