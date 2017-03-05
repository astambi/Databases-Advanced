using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoOOP
{
    class StartupDemoOOP
    {
        static void Main(string[] args)
        {
            Person p1 = new Person()
            {
                Name = "Pesho",
                Age = 20
            };
            Console.WriteLine(p1);

            Person p2 = new Person();
            p2.Name = "Gosho";
            p2.Age = 18;
            Console.WriteLine(p2);

            Person p3 = new Person("Tom");
            Console.WriteLine(p3);

            //uncomment to test fields validation
            //p3.Name = ""; // throws exception
            //p3.Age = -10; // throws exception

            Person p4 = new Person("Tosca", 10);
            Console.WriteLine(p4);
            Person.Greet(); // static method
            p4.Introduce(); // non-static method
        }
    }
}
