using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercisesGringotts
{
    class P14_FirstLetter
    {
        static void Main(string[] args)
        {
            GringottsContext context = new GringottsContext();

            var wizards = context.WizzardDeposits
                .Where(w => w.DepositGroup == "Troll Chest")
                .Select(w => w.FirstName.Substring(0, 1))
                .Distinct()
                .OrderBy(x => x);
            Console.WriteLine(string.Join("\n", wizards));
        }
    }
}