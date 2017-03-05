using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_Students
{
    class P04_Students
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter student names each on a separate line or [End] to terminate:");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "End") break;
                Student student = new Student(input);
            }
            Console.WriteLine($"Number of students: {Student.GetStudentsCount()}");
        }
    }
}