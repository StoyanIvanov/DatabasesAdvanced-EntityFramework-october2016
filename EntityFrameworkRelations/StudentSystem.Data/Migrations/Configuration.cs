using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using StudentSystem.Models;

namespace StudentSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystem.Data.StudentSystemContext>
    {

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "StudentSystem.Data.StudentSystemContext";
        }

        protected override void Seed(StudentSystem.Data.StudentSystemContext context)
        {

            AddHomeworks(context);
            var homeworks = context.Homeworks.ToList();
            AddResources(context);
            var resources = context.Resources.ToList();
            AddCources(context, resources, homeworks);
            var cources = context.Courses.ToList();
            AddStudents(context, cources, homeworks);

        }


        protected void AddHomeworks(StudentSystem.Data.StudentSystemContext context)
        {
            //Add Homeworkd
            if (!context.Resources.Any())
            {
                var fileHomeworks =
                    Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream("StudentSystem.Data.MigrationsData.Homeworks.txt");
                using (var reader = new StreamReader(fileHomeworks))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var homeworkData = line.Split(new[] { ',' }, 3);
                        var homewrokContent = $"{homeworkData[0]}";
                        Homework.ContentTypes contentType = (Homework.ContentTypes)int.Parse(homeworkData[1]);
                        DateTime homeworkSubmissionDate = DateTime.ParseExact(homeworkData[2], "dd-mm-yyyy",
                            null);

                        context.Homeworks.Add(new Homework()
                        {
                            Content = homewrokContent,
                            ContentType = contentType,
                            SubmissionDate = homeworkSubmissionDate
                        });

                        line = reader.ReadLine();
                    }
                    context.SaveChanges();
                }
            }
        }

        protected void AddResources(StudentSystem.Data.StudentSystemContext context)
        {

            //Add Resources
            if (!context.Resources.Any())
            {
                var fileResources =
                Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("StudentSystem.Data.MigrationsData.Resources.txt");
                using (var reader = new StreamReader(fileResources))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var resourceData = line.Split(new[] { ',' }, 4);
                        var resourceName = $"{resourceData[0]}";
                        Resource.ResourcesTypes resourceType = (Resource.ResourcesTypes)int.Parse(resourceData[1]);
                        var resourceURL = resourceData[2];

                        context.Resources.Add(new Resource()
                        {
                            Name = resourceName,
                            Type = resourceType,
                            URL = resourceURL
                        });

                        line = reader.ReadLine();
                    }
                    context.SaveChanges();
                }
            }
        }

        protected void AddCources(StudentSystem.Data.StudentSystemContext context, List<Resource> resources, List<Homework> homeworks  )
        {

            //Add courses
            if (!context.Courses.Any())
            {
                var fileCourses =
                    Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream("StudentSystem.Data.MigrationsData.Courses.txt");
                using (var reader = new StreamReader(fileCourses))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var courseData = line.Split(new[] { ',' }, 7);
                        var courseName = $"{courseData[0]}";
                        var courseDescription = $"{courseData[1]}";
                        DateTime courseStartDate = DateTime.ParseExact(courseData[2], "dd-mm-yyyy", null);
                        DateTime courseEndData = DateTime.ParseExact(courseData[3], "dd-mm-yyyy", null);
                        var coursePrice = decimal.Parse(courseData[4], CultureInfo.InvariantCulture);
                        var courceResources = courseData[5].Split(' ');
                        var courceHomewroks = courseData[6].Split(' ');

                        context.Courses.Add(new Course()
                        {
                            Name = courseName,
                            Description = courseDescription,
                            StartDate = courseStartDate,
                            EndDate = courseEndData,
                            Price = coursePrice,
                            Resources = resources.Where(r => courceResources.Contains(r.ResourceId.ToString())).ToList(),
                            Homeworks = homeworks.Where(h => courceHomewroks.Contains(h.HomeworkId.ToString())).ToList()

                        });

                        line = reader.ReadLine();
                    }
                    context.SaveChanges();

                }
            }
        }

        protected void AddStudents(StudentSystem.Data.StudentSystemContext context, List<Course> cources, List<Homework> homeworks )
        {
            //Add Students
            if (!context.Students.Any())
            {
                var fileStudents =
                    Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream("StudentSystem.Data.MigrationsData.Students.txt");
                using (var reader = new StreamReader(fileStudents))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var studentData = line.Split(new[] { ',' }, 5);
                        var studentName = $"{studentData[0]}";
                        var studentPhone = $"{studentData[1]}";
                        DateTime studentRegistrationDate = DateTime.ParseExact(studentData[2], "dd-mm-yyyy",
                            null);
                        var courseIndexes = studentData[3].Split(new[] { ' ' }, 4);
                        var courses =
                            cources.Where(c => courseIndexes.Contains(c.CourseId.ToString())).ToList();
                        var studentHomewroks = studentData[4].Split(new []{' '}, 4);


                        context.Students.Add(new Student()
                        {
                            Name = studentName,
                            PhoneNumber = studentPhone,
                            RegistrationDate = studentRegistrationDate,
                            Courses = courses,
                            Homeworks = homeworks.Where(h=>studentHomewroks.Contains(h.HomeworkId.ToString())).ToList()
                        });

                        line = reader.ReadLine();
                    }
                }
            }
        }

    }
}
