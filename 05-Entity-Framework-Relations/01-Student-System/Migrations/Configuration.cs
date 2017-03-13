namespace _01_Student_System.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "_01_Student_System.StudentSystemContext";
            AutomaticMigrationDataLossAllowed = false; // Problem 4
        }

        protected override void Seed(StudentSystemContext context) // Problem 2
        {
            //  This method will be called after migrating to the latest version.
            Console.WriteLine("Seeding data");

            // Courses
            context.Courses.AddOrUpdate(c => c.Name,
                new Course()
                {
                    Name = "Databases Advanced",
                    Description = "Entity Framework",
                    StartDate = new DateTime(2017, 2, 20),
                    EndDate = new DateTime(2017, 4, 20),
                    Price = 130.0m,
                    Resources = context.Resources.ToList()
                },
                new Course()
                {
                    Name = "Databases Basics",
                    Description = "MS SQL Server",
                    StartDate = new DateTime(2017, 1, 20),
                    EndDate = new DateTime(2017, 2, 20),
                    Price = 130.0m
                },
                new Course()
                {
                    Name = "JS Core",
                    Description = null,
                    StartDate = new DateTime(2017, 1, 20),
                    EndDate = new DateTime(2017, 4, 20),
                    Price = 390.0m
                });
            context.SaveChanges();

            // Students
            context.Students.AddOrUpdate(p => p.Name,
                new Student
                {
                    Name = "Antoni Gaudi",
                    PhoneNumber = "+359 123456 789",
                    RegistrationDate = DateTime.Now,
                    Birthday = null
                },
                new Student
                {
                    Name = "Tom Smith",
                    PhoneNumber = "+31 123456 789",
                    RegistrationDate = DateTime.Now,
                    Birthday = null
                },
                new Student
                {
                    Name = "Ida Nielsen",
                    PhoneNumber = "+31 455451213",
                    RegistrationDate = DateTime.Now,
                    Birthday = new DateTime(1980, 3, 15)
                },
                new Student
                {
                    Name = "Bob",
                    PhoneNumber = "+31 455451213",
                    RegistrationDate = DateTime.Now,
                    Birthday = new DateTime(1990, 3, 15)
                }
            );
            context.SaveChanges();

            // StudentCourses
            context.Courses.FirstOrDefault(c => c.Name == "Databases Basics").Students.Add(context.Students.Find(1));
            context.Courses.FirstOrDefault(c => c.Name == "Databases Basics").Students.Add(context.Students.Find(2));
            context.Courses.FirstOrDefault(c => c.Name == "Databases Advanced").Students.Add(context.Students.Find(1));
            context.Courses.FirstOrDefault(c => c.Name == "Databases Advanced").Students.Add(context.Students.Find(2));
            context.Courses.FirstOrDefault(c => c.Name == "JS Core").Students.Add(context.Students.Find(1));
            context.Courses.FirstOrDefault(c => c.Name == "JS Core").Students.Add(context.Students.Find(2));
            context.Courses.FirstOrDefault(c => c.Name == "JS Core").Students.Add(context.Students.Find(3));
            context.SaveChanges();

            // Homeworks
            context.Homeworks.AddOrUpdate(h => new { h.Content, h.ContentType, h.StudentId, h.CourseId },
                new Homework()
                {
                    Content = "Exercise: Fetching Resultsets with ADO.NET",
                    ContentType = "zip",
                    SubmissionDate = DateTime.Now,
                    CourseId = context.Courses.FirstOrDefault(c => c.Name.Contains("Databases")).Id,
                    StudentId = context.Students.FirstOrDefault(s => s.Name.Contains("Tom")).Id
                },
                new Homework()
                {
                    Content = "Exercise: Fetching Resultsets with ADO.NET",
                    ContentType = "application",
                    SubmissionDate = DateTime.Now,
                    CourseId = context.Courses.FirstOrDefault(c => c.Name.Contains("Databases")).Id,
                    StudentId = context.Students.FirstOrDefault(s => s.Name.Contains("Gaudi")).Id
                },
                new Homework()
                {
                    Content = "Exercise: Code-First + OOP Intro",
                    ContentType = "zip",
                    SubmissionDate = DateTime.Now,
                    CourseId = context.Courses.FirstOrDefault(c => c.Name.Contains("Databases")).Id,
                    StudentId = context.Students.FirstOrDefault(s => s.Name.Contains("Gaudi")).Id
                },
                new Homework()
                {
                    Content = "Exercise: Code-First (Advanced)",
                    ContentType = "zip",
                    SubmissionDate = DateTime.Now,
                    CourseId = context.Courses.FirstOrDefault(c => c.Name.Contains("Databases")).Id,
                    StudentId = context.Students.FirstOrDefault(s => s.Name.Contains("Gaudi")).Id
                },
                new Homework()
                {
                    Content = "Code First Migrations",
                    ContentType = "zip",
                    SubmissionDate = DateTime.Now,
                    CourseId = context.Courses.FirstOrDefault(c => c.Name.Contains("Databases")).Id,
                    StudentId = context.Students.FirstOrDefault(s => s.Name.Contains("Gaudi")).Id
                });
            context.SaveChanges();

            // Resources
            context.Resources.AddOrUpdate(r => r.Name,
                new Resource()
                {
                    Name = "SPA Book Library",
                    ResourceType = "video",
                    URL = "www.softuni.bg",
                    CourseId = context.Courses.FirstOrDefault(c => c.Name == "JS Core").Id
                },
                new Resource()
                {
                    Name = "Functions, Triggers, Transactions Video",
                    ResourceType = "video",
                    URL = "www.softuni.bg",
                    CourseId = context.Courses.FirstOrDefault(c => c.Name == "Databases Basics").Id
                },
                new Resource()
                {
                    Name = "Entity Relations (OOP Composition) Video",
                    ResourceType = "video",
                    URL = "www.softuni.bg",
                    CourseId = context.Courses.FirstOrDefault(c => c.Name == "Databases Advanced").Id
                },
                new Resource()
                {
                    Name = "Exercise: EntityFramework Relations Video",
                    ResourceType = "video",
                    URL = "www.softuni.bg",
                    CourseId = context.Courses.FirstOrDefault(c => c.Name == "Databases Advanced").Id
                },
                new Resource()
                {
                    Name = "Code First Migrations Video",
                    ResourceType = "video",
                    URL = "www.softuni.bg",
                    CourseId = context.Courses.FirstOrDefault(c => c.Name == "Databases Advanced").Id
                },
                new Resource()
                {
                    Name = "Fetching Resultsets with ADO.NET Video",
                    ResourceType = "video",
                    URL = "www.softuni.bg",
                    CourseId = context.Courses.FirstOrDefault(c => c.Name == "Databases Advanced").Id
                },
                new Resource()
                {
                    Name = "Code-First + OOP Intro Video",
                    ResourceType = "video",
                    URL = "www.softuni.bg",
                    CourseId = context.Courses.FirstOrDefault(c => c.Name == "Databases Advanced").Id
                },
                new Resource()
                {
                    Name = "Code First Migrations Presentation",
                    ResourceType = "presentation",
                    URL = "www.softuni.bg",
                    CourseId = context.Courses.FirstOrDefault(c => c.Name == "Databases Advanced").Id
                });
            context.SaveChanges();

            // Problem 4
            context.Licences.AddOrUpdate(r => new { r.Name, r.ResourceId },
                new License() { Name = "MIT", ResourceId = 1 },
                new License() { Name = "MIT", ResourceId = 2 },
                new License() { Name = "MIT", ResourceId = 3 },
                new License() { Name = "No License", ResourceId = 4 },
                new License() { Name = "No License", ResourceId = 5 },
                new License() { Name = "No License", ResourceId = 6 }
                );
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
