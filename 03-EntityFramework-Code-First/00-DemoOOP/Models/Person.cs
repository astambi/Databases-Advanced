using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoOOP
{
    class Person
    {
        private string name; // default value => ""
        private int age; // default value => 0

        public Person()
        {
        }

        public Person(string name)
        {
            // this.Name => validation, this.name => no validation
            this.Name = name;
        }

        public Person(string name, int age) : this(name) // chained constructor
        {
            this.Age = age;
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Invalid name");
                this.name = value;
            }
        }

        public int Age
        {
            get { return this.age; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Invalid age");
                this.age = value;
            }
        }
        public override string ToString()
        {
            return Name + " " + Age;
        }

        public static void Greet()
        {
            Console.WriteLine("Hello!");
        }

        public void Introduce()
        {
            Console.WriteLine($"Hello! My name is {this.name} and I am {this.age} years old.");
        }
    }
}
