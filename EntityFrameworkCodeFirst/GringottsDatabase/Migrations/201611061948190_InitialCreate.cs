namespace GringottsDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        Image = c.Binary(storeType: "image"),
                        RegisteredOn = c.DateTime(nullable: false),
                        LastTimeLoggedIn = c.DateTime(),
                        Age = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WizardDeposits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 60),
                        Notes = c.String(maxLength: 1000),
                        Age = c.Int(nullable: false),
                        MagicWandCreator = c.String(maxLength: 100),
                        MagicWandSize = c.Int(nullable: false),
                        DepositGroup = c.String(maxLength: 20),
                        DepositStartDate = c.DateTime(nullable: false),
                        DepositExpirationDate = c.DateTime(nullable: false),
                        DepositAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DepositCharge = c.Double(nullable: false),
                        IsDepositeExpired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WizardDeposits");
            DropTable("dbo.Users");
        }
    }
}
