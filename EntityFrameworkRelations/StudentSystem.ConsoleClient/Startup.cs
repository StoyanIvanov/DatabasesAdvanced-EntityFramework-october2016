using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
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
            //
            //var students = context.Students.ToList();
            //foreach (var student in students)
            //{
            //    Console.WriteLine($"{student.Name}");
            //    foreach (var homework in student.Homeworks)
            //    {
            //        Console.WriteLine($"   -{homework.Content} {homework.ContentType}");
            //    }
            //}

            //2.	List all courses with their corresponding resources. Print the course name and description and everything for each resource. 
            //Order the courses by start date (ascending), then by end date (descending).
            //
            //using (context)
            //{
            //    var cources = context.Courses
            //                         .OrderBy(c=>c.StartDate)
            //                         .ThenByDescending(c=>c.EndDate);
            //
            //    foreach (var cource in cources)
            //    {
            //        Console.WriteLine($"{cource.Name} {cource.Description}");
            //        foreach (var resource in cource.Resources)
            //        {
            //            Console.WriteLine($"   -{resource.Name}");
            //        }
            //    }
            //}

            //3.	List all courses with more than 5 resources. Order them by resources count (descending), then by start date (descending). 
            //Print only the course name and the resource count.
            //
            //using (context)
            //{
            //    var cources = context.Courses
            //                         .Where(c => c.Resources.Count > 5)
            //                         .OrderByDescending(c => c.Resources.Count)
            //                         .ThenByDescending(c => c.StartDate)
            //                         .Select(c => new {c.Name, c.Resources.Count});
            //
            //    foreach (var cource in cources)
            //    {
            //        Console.WriteLine($"{cource.Name} {cource.Count}");
            //    }
            //
            //}

            //4.	List all courses which were active on a given date (choose the date depending on the data seeded to ensure there are results), 
            //and for each course count the number of students enrolled.
            //Print the course name, start and end date, course duration (difference between end and start date) and number of students enrolled.
            //Order the results by the number of students enrolled(in descending order), then by duration(descending).
            //
            //var date=new DateTime(2016,2,28);
            //using (context)
            //{
            //    var cources = context.Courses
            //                         .Where(c=>c.StartDate<=date && c.EndDate<=date && c.Students.Count>0)
            //                         .Select(c=>new {c.Name,c.StartDate,c.EndDate,SqlFunctions.DateDiff("dd", c.StartDate, c.EndDate).Value, c.Students.Count})
            //                         .OrderByDescending(c=>c.Count)
            //                         .ThenByDescending(c=>c.Value)
            //                         .ToList();
            //
            //    foreach (var cource in cources)
            //    {
            //        Console.WriteLine($"{cource.Name} {cource.StartDate} {cource.EndDate} {cource.Value} {cource.Count}");
            //    }
            //}

            //5.	For each student, calculate the number of courses he/she has enrolled in, 
            //the total price of these courses and the average price per course for the student. Print the student name, number of courses, total price and average price. 
            //Order the results by total price (descending), then by number of courses(descending) and then by the student's name (ascending).
            //
            //using (context)
            //{
            //    var students = context.Students
            //                          .Select(s => new {s.Name, s.Courses.Count, totalPrice=s.Courses.Sum(c => c.Price), avgPrice = s.Courses.Average(c=>c.Price) });
            //    foreach (var student in students)
            //    {
            //        Console.WriteLine($"{student.Name} / {student.Count} / {student.totalPrice} / {student.avgPrice}");
            //    }
            //}

        }
    }
}
