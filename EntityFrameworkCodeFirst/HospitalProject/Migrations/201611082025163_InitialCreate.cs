namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Diagnoses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Comments = c.String(maxLength: 8000, unicode: false),
                        Patient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.Patient_Id)
                .Index(t => t.Patient_Id);
            
            CreateTable(
                "dbo.Medicaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Patient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.Patient_Id)
                .Index(t => t.Patient_Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50, unicode: false),
                        LastName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Address = c.String(maxLength: 250, unicode: false),
                        Email = c.String(nullable: false),
                        BurthDate = c.DateTime(nullable: false),
                        Image = c.Binary(storeType: "image"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Visitations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitationDate = c.DateTime(nullable: false),
                        Comments = c.String(maxLength: 8000, unicode: false),
                        Patient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.Patient_Id)
                .Index(t => t.Patient_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visitations", "Patient_Id", "dbo.Patients");
            DropForeignKey("dbo.Medicaments", "Patient_Id", "dbo.Patients");
            DropForeignKey("dbo.Diagnoses", "Patient_Id", "dbo.Patients");
            DropIndex("dbo.Visitations", new[] { "Patient_Id" });
            DropIndex("dbo.Medicaments", new[] { "Patient_Id" });
            DropIndex("dbo.Diagnoses", new[] { "Patient_Id" });
            DropTable("dbo.Visitations");
            DropTable("dbo.Patients");
            DropTable("dbo.Medicaments");
            DropTable("dbo.Diagnoses");
        }
    }
}
