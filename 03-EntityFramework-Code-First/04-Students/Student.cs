using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_Students
{
    class Student
    {
        private static int studentsCount; 

        public Student(string name)
        {
            this.Name = name;
            studentsCount++; // static => no this
        }

        public string Name { get; set; }

        public static int GetStudentsCount()
        {
            return studentsCount;
        }
    }
}