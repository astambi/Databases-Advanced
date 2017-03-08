using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_Planck_Constant
{
    class Calculation
    {
        private static double planckConstant = 6.62606896e-34; // not decimal !
        private static double pi = 3.14159;

        public static double GetReducedPlanckConstant()
        {
            return planckConstant / (2 * pi);
        }
    }
}