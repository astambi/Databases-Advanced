using _01_Student_System.Models;
using System;
using System.Linq;

namespace _01_Student_System
{
    class P01_StudentSystem
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initialiazing database [StudentSystem.CodeFirst]");
            StudentSystemContext context = new StudentSystemContext();
            context.Database.Initialize(true);

            Console.WriteLine("\nSolutions to Problems:\n" +
                "1. Code First Student System\n" +
                "2. Seed Some Data in the Database\n" +
                "3. Working with the Database\n" +
                "4. Resource Licenses\n\n" +
                "Rollback migrations to see previous versions of the database.\n");

            WorkingWithTheDatabase(context); // Problem 3
        }

        private static void WorkingWithTheDatabase(StudentSystemContext context)
        {
            while (true)
            {
                Console.WriteLine("\n************************************************************");
                Console.WriteLine("Solutions to Problem 3:\n" +
                    "1. List all students & their homework submissions\n" +
                    "2. List all courses & their corresponding resources\n" +
                    "3. List all courses with more than 5 resources\n" +
                    "4.*List all courses active on a given date & number of students enrolled\n" +
                    "5. List all students with their number of courses, total price & average price"
                    );
                Console.Write("Please choose an option or type [end] to terminate program: ");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1": ListAllStudents(context); break;
                    case "2": ListAllCourses(context); break;
                    case "3": ListCoursesWithMoreThen5Resources(context); break;
                    case "4": ListActiveCourses(context); break;
                    case "5": ListStudentsCoursesWithPrice(context); break;
                    case "end": break;
                    default: Console.WriteLine("Invalid option"); break;
                }
                if (option == "end") break;
            }
        }

        private static void ListActiveCourses(StudentSystemContext context)
        {
            Console.Clear();

            Console.Write("Select a date between 20/01/2017 & 20/04/2017: ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            var courses = context.Courses
                .Where(c => c.StartDate <= date && c.EndDate >= date)
                .Select(c => new
                {
                    c.Name,
                    c.StartDate,
                    c.EndDate,
                    NumberOfStudents = c.Students.Count
                }).ToList()
                .OrderByDescending(c => c.NumberOfStudents)
                .ThenByDescending(c => c.EndDate - c.StartDate)
                .ToList();

            Console.WriteLine($"Listing Courses Active on {date.ToString("dd/MM/yyy")} - {courses.Count()} Course(s)");
            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Name} [{course.StartDate.ToString("dd/MM/yyy")} - {course.EndDate.ToString("dd/MM/yyy")}], Duration {(course.EndDate - course.StartDate).Days} days, {course.NumberOfStudents} Students");
            }
        }

        private static void ListStudentsCoursesWithPrice(StudentSystemContext context)
        {
            Console.Clear();

            // students with some courses
            var studentsWithCourses = context.Students
                .Where(s => s.Courses.Count > 0)
                .Select(s => new
                {
                    s.Name,
                    NumberOfCourses = s.Courses.Count,
                    TotalPrice = s.Courses.Sum(c => c.Price)
                })
                .OrderByDescending(s => s.TotalPrice)
                .ThenByDescending(s => s.NumberOfCourses)
                .ThenBy(s => s.Name);

            Console.WriteLine($"Listing Students with Number of Courses, Total Price & Average Price per Course");
            foreach (var student in studentsWithCourses)
            {
                Console.WriteLine($"{student.Name}, {student.NumberOfCourses} Courses, Total Price {student.TotalPrice:f2}, Average Price {student.TotalPrice / student.NumberOfCourses:f2}");
            }

            // students without courses ordered by Name only
            context.Students
                .Where(s => s.Courses.Count == 0)
                .OrderBy(s => s.Name).ToList()
                .ForEach(s => Console.WriteLine($"{s.Name}, 0 Courses, Total Price 0.00, Average Price 0.00"));
        }

        private static void ListCoursesWithMoreThen5Resources(StudentSystemContext context)
        {
            Console.Clear();

            var courses = context.Courses
                .Where(c => c.Resources.Count > 5) // number of recourses 
                .OrderByDescending(c => c.Resources.Count)
                .ThenByDescending(c => c.StartDate)
                .Select(c => new { c.Name, ResourcesCount = c.Resources.Count });

            Console.WriteLine($"Listing Courses with more than 5 resources - {courses.Count()} Course(s)");
            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Name} - {course.ResourcesCount} Resources");
            }
        }

        private static void ListAllCourses(StudentSystemContext context)
        {
            Console.Clear();

            var courses = context.Courses
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .Select(c => new { c.Name, c.Description, c.Resources });

            Console.WriteLine("Listing Courses (name, description) & corresponding Resources");

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Name} ({ course.Description ?? "No Description"}) - {course.Resources.Count} Resource(s)");
                foreach (Resource resource in course.Resources)
                {
                    Console.WriteLine($"   {resource.Name} ({resource.ResourceType}) - {resource.URL}");
                }
            }
        }

        private static void ListAllStudents(StudentSystemContext context)
        {
            Console.Clear();

            var students = context.Students.Select(s => new { s.Name, s.Homeworks });
            Console.WriteLine("Listing Students & their Homework Submissions");

            foreach (var student in students)
            {
                Console.WriteLine($"{student.Name} - { student.Homeworks.Count} Homework Submissions");
                foreach (Homework homework in student.Homeworks)
                {
                    Console.WriteLine($"   {homework.Content} ({homework.ContentType})");
                }
            }
        }
    }
}
