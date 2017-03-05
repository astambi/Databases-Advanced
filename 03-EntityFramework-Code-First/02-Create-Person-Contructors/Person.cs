using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Create_Person_Contructors
{
    public class Person
    {
        private string name;
        private int age;

        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }
        public Person() : this("No name", 1)
        {
        }
        public Person(string name) : this(name, 1)
        {
        }
        public Person(int age) : this("No name", age)
        {
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    this.name = "No name";
                else this.name = value.Trim();
            }
        }

        public int Age
        {
            get { return this.age; }
            set
            {
                if (value < 0)
                    this.age = 1;
                else this.age = value;
            }
        }

        public override string ToString()
        {
            return this.name + " " + this.age;
        }
    }
}