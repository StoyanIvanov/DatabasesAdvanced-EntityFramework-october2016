using GringottsDatabase.Models;

namespace GringottsDatabase
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class GringottsContext : DbContext
    {
        // Your context has been configured to use a 'GringottsContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'GringottsDatabase.GringottsContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'GringottsContext' 
        // connection string in the application configuration file.
        public GringottsContext()
            : base("name=GringottsContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<GringottsContext>() );
        }

        public DbSet<WizardDeposits> WizardDepositses { get; set; }
        public DbSet<User> Users { get; set; }
      
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}