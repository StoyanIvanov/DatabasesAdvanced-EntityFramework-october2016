
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using SoftUni.Models;

namespace SoftUni
{
    public class Startup
    {
        static void Main()
        {
            SoftUniContext context = new SoftUniContext();

            using (context)
            {
                //SELECT
                //                Address address = context.Addresses.Find(2);
                //                string address = context.Addresses.Where(a => a.AddressID == 2).Select(a => a.AddressText).FirstOrDefault();


                //UPDATE

                //Town town = new Town {Name = "Pernik"};
                //context.Towns.Add(town);
                //context.SaveChanges();
                //
                //Address newAddress = new Address {AddressText = "my new address"};
                //newAddress.TownID = context.Towns
                //    .Where(t => t.Name == "Pernik")
                //    .Select(t => t.TownID)
                //    .FirstOrDefault();
                //town.Addresses.Add(newAddress);
                //context.SaveChanges();

                //REMOVE

                //Town town = context.Towns.FirstOrDefault(t => t.TownID==32);
                //context.Towns.Remove(town);
                //context.Addresses.RemoveRange(town.Addresses);
                //context.SaveChanges();


                //Best practice
                //Town town = context.Towns.FirstOrDefault(t => t.TownID==32);
                //foreach (var address in town.Addresses)
                //{
                //    address.TownID = null;
                //}
                //context.Towns.Remove(town);
                //context.SaveChanges();
                //

                //HOMEWORK

                //3.	Employees full information
                //IEnumerable<Employee> employees = context.Employees;
                //foreach (var employee in employees)
                //{
                //    Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary.ToString(CultureInfo.GetCultureInfo("en-GB"))}");
                //}

                //4.	Employees with Salary Over 50 000
                //IEnumerable<string> employees = context.Employees.Where(e => e.Salary > 50000).Select(e=>e.FirstName).ToList();
                //foreach (var employee in employees)
                //{
                //    Console.WriteLine(employee);
                //}

                //5.	Employees from Seattle
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
                IEnumerable<Employee> employees = context.Employees
                    .Where(e => e.Department.Name == "Research and Development")
                    .OrderBy(e => e.Salary)
                    .ThenByDescending(e => e.FirstName)
                    .ToList();

                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} from {employee.Department.Name} - ${employee.Salary:F2}");
                }
            }
        }
    }
}
