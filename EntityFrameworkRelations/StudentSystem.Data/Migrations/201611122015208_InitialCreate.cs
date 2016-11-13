namespace StudentSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.Homework",
                c => new
                    {
                        HomeworkId = c.Int(nullable: false, identity: true),
                        content = c.String(),
                        ContentType = c.Int(nullable: false),
                        SubmissionDate = c.DateTime(nullable: false),
                        Cource_CourseId = c.Int(),
                        Student_StudentId = c.Int(),
                    })
                .PrimaryKey(t => t.HomeworkId)
                .ForeignKey("dbo.Courses", t => t.Cource_CourseId)
                .ForeignKey("dbo.Students", t => t.Student_StudentId)
                .Index(t => t.Cource_CourseId)
                .Index(t => t.Student_StudentId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        PhoneNumber = c.String(),
                        RegistrationDate = c.DateTime(nullable: false),
                        BurthDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.StudentId);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        ResourceId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                        URL = c.String(nullable: false),
                        Cource_CourseId = c.Int(),
                    })
                .PrimaryKey(t => t.ResourceId)
                .ForeignKey("dbo.Courses", t => t.Cource_CourseId)
                .Index(t => t.Cource_CourseId);
            
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        Student_StudentId = c.Int(nullable: false),
                        Course_CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_StudentId, t.Course_CourseId })
                .ForeignKey("dbo.Students", t => t.Student_StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_CourseId, cascadeDelete: true)
                .Index(t => t.Student_StudentId)
                .Index(t => t.Course_CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resources", "Cource_CourseId", "dbo.Courses");
            DropForeignKey("dbo.Homework", "Student_StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentCourses", "Course_CourseId", "dbo.Courses");
            DropForeignKey("dbo.StudentCourses", "Student_StudentId", "dbo.Students");
            DropForeignKey("dbo.Homework", "Cource_CourseId", "dbo.Courses");
            DropIndex("dbo.StudentCourses", new[] { "Course_CourseId" });
            DropIndex("dbo.StudentCourses", new[] { "Student_StudentId" });
            DropIndex("dbo.Resources", new[] { "Cource_CourseId" });
            DropIndex("dbo.Homework", new[] { "Student_StudentId" });
            DropIndex("dbo.Homework", new[] { "Cource_CourseId" });
            DropTable("dbo.StudentCourses");
            DropTable("dbo.Resources");
            DropTable("dbo.Students");
            DropTable("dbo.Homework");
            DropTable("dbo.Courses");
        }
    }
}
