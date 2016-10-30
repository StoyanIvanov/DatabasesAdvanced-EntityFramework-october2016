using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gringotts.Models;

namespace Gringotts
{
    public class Startup
    {
        static void Main()
        {
            GringottsContext context = new GringottsContext();

            using (context)
            {
                IEnumerable<string> wizzardDeposits = context.WizzardDeposits
                    .Where(wd => wd.DepositGroup == "Troll Chest")
                    .Select(wd => wd.FirstName.Substring(0, 1))
                    .Distinct()
                    .ToList();
                foreach (var wizzardDeposit in wizzardDeposits)
                {
                    Console.WriteLine($"{wizzardDeposit}");
                }
            }
        }

    }
}
