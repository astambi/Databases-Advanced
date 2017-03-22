using Demo.Data;
using System;
using System.Linq;

namespace Demo.Client
{
    class DemoStartup
    {
        static void Main(string[] args)
        {
            using (DemoContext context = new DemoContext())
            {
                Console.WriteLine("Initializing database");
                context.Database.Initialize(true);

                context.Persons
                    .Select(p => p.Name)
                    .ToList()
                    .ForEach(p => Console.WriteLine(p));







            }
        }
    }
}
