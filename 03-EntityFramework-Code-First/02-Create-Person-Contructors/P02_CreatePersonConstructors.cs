using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _02_Create_Person_Contructors
{
    class P02_CreatePersonConstructors
    {
        static void Main(string[] args)
        {
            // uncomment to test examples
            //Console.WriteLine(GetPerson("Pesho,20"));
            //Console.WriteLine(GetPerson("Gosho"));
            //Console.WriteLine(GetPerson("43"));
            //Console.WriteLine(GetPerson("      ")); // validation name, age
            //Console.WriteLine(GetPerson("   ,10")); // validation name
            //Console.WriteLine(GetPerson("  ,-10")); // validation name, age
            //Console.WriteLine(GetPerson("-10")); // validation name, age
            //Console.WriteLine(GetPerson()); // validation name, age

            Console.WriteLine("Please enter a person ([name,age] / [name] / [age] / empty): ");
            Console.WriteLine(GetPerson((Console.ReadLine())));
        }

        private static Person GetPerson(string input = "")
        {
            string[] inputArgs = input.Split(',').ToArray();
            Person person = new Person();

            switch (inputArgs.Length)
            {
                case 2:
                    person = new Person(inputArgs[0], int.Parse(inputArgs[1]));
                    break;
                case 1:
                    if (Regex.IsMatch(inputArgs[0], @"\d"))
                        person = new Person(int.Parse(inputArgs[0]));
                    else
                        person = new Person(inputArgs[0]);
                    break;
                default: break;
            }
            return person;
        }
    }
}