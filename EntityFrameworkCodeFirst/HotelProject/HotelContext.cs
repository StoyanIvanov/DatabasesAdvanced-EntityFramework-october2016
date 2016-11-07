using System.Security.AccessControl;
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
        public DbSet<Customers> Customerses { get; set; }
        public DbSet<RoomStatuses> RoomStatuses { get; set; }

        public DbSet<RoomTypes> RoomTypeses { get; set; }

        public DbSet<BedTypes> BedTypeses { get; set; }

        public DbSet<Rooms> Rooms { get; set; }

        public DbSet<Payments> Payments { get; set; }

        public DbSet<Occupancies> Occupancies { get; set; }

    }

}