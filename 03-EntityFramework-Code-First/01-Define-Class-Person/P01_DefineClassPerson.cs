using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Define_Class_Person
{
    class P01_DefineClassPerson
    {
        static void Main(string[] args)
        {
            Person p1 = new Person("Pesho", 20);
            Console.WriteLine(p1);

            Person p2 = new Person()
            {
                Name = "Gosho",
                Age = 18
            };
            Console.WriteLine(p2);

            Person p3 = new Person();
            p3.Name = "Stamat";
            p3.Age = 43;
            Console.WriteLine(p3);
        }
    }
}