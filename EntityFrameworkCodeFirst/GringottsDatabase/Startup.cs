using System;
using System.Data;
using GringottsDatabase.Models;

namespace GringottsDatabase
{
    class Startup
    {
        public static void Main()
        {
            GringottsContext context = new GringottsContext();
            WizardDeposits deposit = new WizardDeposits
            {
                FirstName = "Stoyan",
                LastName = "Ivanov",
                Age = 37,
                MagicWandCreator = "magic creator",
                MagicWandSize = 15,
                DepositStartDate = new DateTime(2016, 10, 20),
                DepositExpirationDate = new DateTime(2020, 10, 20),
                DepositAmount = 20000.24m,
                DepositCharge = 0.2,
                IsDepositeExpired = false
            };
            User user=new User
            {
                Username = "saivanov",
                Password = "Yahoocom@2",
                Email = "saivanovs@gmail.com",
                Age = 37,
                IsDeleted = false,
                RegisteredOn = DateTime.Now
            };

            context.Users.Add(user);
            context.WizardDepositses.Add(deposit);
            context.SaveChanges();

        }
    }
}
