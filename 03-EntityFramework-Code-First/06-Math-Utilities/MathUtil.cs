using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_Math_Utilities
{
    class MathUtil
    {
        public static decimal Sum(decimal num1, decimal num2)
        {
            return num1 + num2;
        }

        public static decimal Subtract(decimal num1, decimal num2)
        {
            return num1 - num2;
        }

        public static decimal Multiply(decimal num1, decimal num2)
        {
            return num1 * num2;
        }

        public static decimal? Divide(decimal num1, decimal num2)
        {
            if (num2 == 0) return null;
            return num1 / num2;
        }

        public static decimal Percentage(decimal total, decimal percentage)
        {
            return Multiply(total, percentage) / 100m;
        }
    }
}