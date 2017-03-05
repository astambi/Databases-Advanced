using _02_Create_Person_Contructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Oldest_Family_Member
{
    class Family
    {
        private List<Person> members;

        public Family()
        {
            this.members = new List<Person>();
        }

        public List<Person> Members
        {
            get { return this.members; }
            set
            {
                this.Members = this.members;
            }
        }

        public void AddMember(Person member)
        {
            this.members.Add(member);
        }

        public List<Person> GetOldestMember()
        {
            int maxAge = members.Max(m => m.Age);
            return members.Where(m => m.Age == maxAge).OrderBy(m => m.Name).ToList();
        }

        public void Print()
        {
            if (this.members.Count > 0)
            {
                Console.WriteLine("Family members:");
                members.ForEach(m => Console.WriteLine(m));
            }
            else Console.WriteLine("No family members.");
        }
    }
}