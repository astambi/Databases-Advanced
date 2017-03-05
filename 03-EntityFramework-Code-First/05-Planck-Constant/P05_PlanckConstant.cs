using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_Planck_Constant
{
    class P05_PlanckConstant
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Reduced Planck constant = {Calculation.GetReducedPlanckConstant()}");
        }
    }
}