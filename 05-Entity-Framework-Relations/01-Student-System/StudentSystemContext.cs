namespace _01_Student_System
{
    using Migrations;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
            : base("name=StudentSystemContext")
        {
            //Database.SetInitializer<StudentSystemContext>(new DropCreateDatabaseAlways<StudentSystemContext>());

            Database.SetInitializer<StudentSystemContext>(new MigrateDatabaseToLatestVersion<StudentSystemContext, Configuration>()); // Problem 2
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<Homework> Homeworks { get; set; }
        public virtual DbSet<License> Licences { get; set; } // Problem 4
    }
}