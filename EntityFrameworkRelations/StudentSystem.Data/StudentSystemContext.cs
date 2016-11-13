using StudentSystem.Models;

namespace StudentSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class StudentSystemContext : DbContext
    {
        // Your context has been configured to use a 'StudentSystemContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'StudentSystem.Data.StudentSystemContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'StudentSystemContext' 
        // connection string in the application configuration file.
        public StudentSystemContext()
            : base("name=StudentSystemContext")
        {

        }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Course> Courses { get; set; }



    }
}