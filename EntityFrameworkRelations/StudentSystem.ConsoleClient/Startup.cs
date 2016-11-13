using System;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using StudentSystem.Data;
using StudentSystem.Models;

namespace StudentSystem.ConsoleClient
{
    public class Startup
    {
        static void Main(string[] args)
        {
            StudentSystemContext context = new StudentSystemContext();
            //var migrationStrategy = new DropCreateDatabaseIfModelChanges<StudentSystemContext>();
            //Database.SetInitializer(migrationStrategy);

            context.Database.Initialize(true);

            //Lists all students and their homework submissions. Print only their names and for each homework - 
            //content and content - type.
            //var students = context.Students.ToList();
            //foreach (var student in students)
            //{
            //    Console.WriteLine($"{student.Name}");
            //    foreach (var homework in student.Homeworks)
            //    {
            //        Console.WriteLine($"   -{homework.Content} {homework.ContentType}");
            //    }
            //}

        }
    }
}
