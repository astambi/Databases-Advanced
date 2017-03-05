using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_Planck_Constant
{
    class Calculation
    {
        private static decimal planckConstant = 6.62606896e-34m;
        private static decimal pi = 3.14159m;

        public static decimal GetReducedPlanckConstant()
        {
            return planckConstant / (2m * pi);
        }
    }
}