using HotelProject.Models;

namespace HotelProject
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class HotelContext : DbContext
    {
        // Your context has been configured to use a 'HotelContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'HotelProject.HotelContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'HotelContext' 
        // connection string in the application configuration file.
        public HotelContext()
            : base("name=HotelContext")
        {

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<HotelContext>());
        }

        public DbSet<Employees> Employeeses { get; set; }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}