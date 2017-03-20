using GringottsDB.Models;
using System;
using System.IO;
using System.Linq;

namespace GringottsDB
{
    class GringottsStartup
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Solutions to Advanced Querying - Gringotts database\n" +
                "Please import Gringotts dababase & if necessary modify the connection string\n");
            GringottsContext context = new GringottsContext();

            // Solutions
            DepositsSumForOllivanderFamilyLinq(context);
            DepositsSumForOllivanderFamilyNativeSql(context);
            DepositsFilterNativeSql(context);
        }

        private static void DepositsFilterNativeSql(GringottsContext context)
        {
            Console.WriteLine("Solution to Problem. Deposits Filter (using native SQL), sum < 150000 DESC\n");
            DepositsSumNativeSql(context, "GetDepositsSumFiltered");
        }

        private static void DepositsSumForOllivanderFamilyNativeSql(GringottsContext context)
        {
            Console.WriteLine("Solution to Problem. Deposits Sum for Ollivander Family (using native SQL)\n");
            DepositsSumNativeSql(context, "GetDepositsSum");
        }

        private static void DepositsSumNativeSql(GringottsContext context, string fileName)
        {
            string sqlQueryDepositsSum = File.ReadAllText($"../../SqlQueries/{fileName}.sql");
            var depositsSums = context.Database.SqlQuery<DepositSum>(sqlQueryDepositsSum);

            foreach (DepositSum g in depositsSums)
            {
                Console.WriteLine($"{g.DepositGroup} - {g.DepositsSum:f2}");
            }
            Pause();
        }

        private static void DepositsSumForOllivanderFamilyLinq(GringottsContext context)
        {
            Console.WriteLine("Solution to Problem. Deposits Sum for Ollivander Family (using EF & LINQ)\n");
            var depositGroups = context.WizzardDeposits
                .Select(d => d.DepositGroup)
                .Distinct();
            foreach (string depositGroup in depositGroups)
            {
                decimal depositSum = context.WizzardDeposits
                    .Where(d => d.DepositGroup == depositGroup && d.MagicWandCreator == "Ollivander family")
                    .Sum(d => d.DepositAmount) ?? 0;
                Console.WriteLine($"{depositGroup} - {depositSum:f2}");
            }
            Pause();
        }

        private static void Pause()
        {
            Console.WriteLine("\nPress any key to continue with the next problem");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
