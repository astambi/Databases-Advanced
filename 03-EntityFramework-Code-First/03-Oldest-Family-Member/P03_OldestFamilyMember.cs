using _02_Create_Person_Contructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Oldest_Family_Member
{
    class P03_OldestFamilyMember
    {
        static void Main(string[] args)
        {
            Family family = GetFamily();
            PrintOldestMember(family);
        }

        private static Family GetFamily()
        {
            Console.WriteLine("Please enter family [size] & family members [name age] each on a seperate line:");
            int familySize = int.Parse(Console.ReadLine());

            Family family = new Family();
            //family.Print();
            for (int i = 0; i < familySize; i++)
            {
                string[] input = Console.ReadLine().Split(' ').ToArray();
                string name = input[0];
                int age = int.Parse(input[1]);
                family.AddMember(new Person(name, age));
            }
            //family.Print();
            return family;
        }

        private static void PrintOldestMember(Family family)
        {
            if (family.Members.Count > 0)
            {
                Console.WriteLine("Oldest member(s):"); // if several, print all ordered by name ASC
                family.GetOldestMember().ForEach(m => Console.WriteLine(m));
            }
            else Console.WriteLine("No family members.");
        }
    }
}