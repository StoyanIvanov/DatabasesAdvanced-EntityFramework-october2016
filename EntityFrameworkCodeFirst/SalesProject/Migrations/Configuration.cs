using SalesProject.Models;

namespace SalesProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SalesProject.SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            
        }

        protected override void Seed(SalesProject.SalesContext context)
        {
            if (!context.Customers.Any())
            {
                Customer customer = new Customer
                {
                    Name = "Pesho",
                    Email = "email@email.email",
                    CreditCardNumber = "1234567890"
                };

                context.Customers.Add(customer);
                context.SaveChanges();
            }
            
        }
    }
}
