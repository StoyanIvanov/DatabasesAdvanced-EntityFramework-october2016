using System;
using GringottsDatabase.Attributes;
using GringottsDatabase.Models;

namespace GringottsDatabase
{
    public class Startup
    {
        static void Main()
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
            User user = new User
            {
                Username = "saivanov",
                Password = "Yahoocom@2",
                Email = "saivanovs@gmail.com",
                Age = 37,
                IsDeleted = false,
                RegisteredOn = DateTime.Now
            };

            Album album = new Album()
            {
                Name = "FirstAlbum",
                
            };

            user.Albums.Add(album);
            //context.WizardDepositses.Add(deposit);
            context.Users.Add(user);
            context.SaveChanges();


        }
    }
}
