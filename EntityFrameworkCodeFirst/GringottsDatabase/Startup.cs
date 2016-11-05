using System;
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
                MagicWandCreator = "magic creator",
                MagicWandSize = 15,
                DepositStartDate = new DateTime(2016, 10, 20),
                DepositExpirationDate = new DateTime(2020, 10, 20),
                DepositAmount = 20000.24m,
                DepositCharge = 0.2,
                IsDepositeExpired = false
            };

            context.WizardDepositses.Add(deposit);
            context.SaveChanges();

        }
    }
}
