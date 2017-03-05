using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_Math_Utilities
{
    class P06_MathUtilities
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter commands [cmd num1 num2] or [End] to terminate:");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "End") break;

                var tokens = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                decimal num1 = decimal.Parse(tokens[1]);
                decimal num2 = decimal.Parse(tokens[2]);
                decimal? result = null; // invalid calculation, e.g number / 0, unknown command

                switch (tokens[0])
                {
                    case "Sum":
                        result = MathUtil.Sum(num1, num2); break;
                    case "Subtract":
                        result = MathUtil.Subtract(num1, num2); break;
                    case "Multiply":
                        result = MathUtil.Multiply(num1, num2); break;
                    case "Divide":
                        result = MathUtil.Divide(num1, num2); break;
                    case "Percentage":
                        result = MathUtil.Percentage(num1, num2); break;
                    default: break;
                }
                if (result != null)
                    Console.WriteLine($"{result:f2}");
                else Console.WriteLine("Invalid calculation");
            }
        }
    }
}