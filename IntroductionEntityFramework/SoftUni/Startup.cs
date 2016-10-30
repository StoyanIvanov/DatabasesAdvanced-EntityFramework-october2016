
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
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
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

                ////3.	Employees full information
                //
                //IEnumerable<Employee> employees = context.Employees;
                //foreach (var employee in employees)
                //{
                //    Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary.ToString(CultureInfo.GetCultureInfo("en-GB"))}");
                //}

                ////4.	Employees with Salary Over 50 000
                //
                //IEnumerable<string> employees = context.Employees.Where(e => e.Salary > 50000).Select(e=>e.FirstName).ToList();
                //foreach (var employee in employees)
                //{
                //    Console.WriteLine(employee);
                //}

                ////5.	Employees from Seattle
                //
                //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
                //IEnumerable<Employee> employees = context.Employees
                //    .Where(e => e.Department.Name == "Research and Development")
                //    .OrderBy(e => e.Salary)
                //    .ThenByDescending(e => e.FirstName)
                //    .ToList();
                //
                //foreach (var employee in employees)
                //{
                //    Console.WriteLine($"{employee.FirstName} {employee.LastName} from {employee.Department.Name} - ${employee.Salary:F2}");
                //}

                ////6.	Adding a New Address and Updating Employee
                //
                //Address address = new Address {AddressText = "Vitoshka 15", TownID = 4};
                //Employee employee = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");
                //employee.Address = address;
                //context.SaveChanges();
                //
                //IEnumerable<string> employees = context.Employees
                //    .OrderByDescending(e => e.AddressID)
                //    .Select(e => e.Address.AddressText)
                //    .Take(10);
                //
                //foreach (var employee1 in employees)
                //{
                //    Console.WriteLine($"{employee1}");
                //}

                ////7.	Delete Project by Id
                //
                //Project project = context.Projects.Find(2);
                //foreach (var employee in project.Employees)
                //{
                //    employee.Projects = null;
                //}
                //context.Projects.Remove(project);
                //context.SaveChanges();
                //
                //IEnumerable<string> projects = context.Projects.Select(p => p.Name).Take(10);
                //foreach (var project1 in projects)
                //{
                //    Console.WriteLine($"{project1}");
                //}

                ////8.	Find employees in period
                //
                //DateTime startDate = new DateTime(2001,01,01);
                //DateTime endDate = new DateTime(2003,12,31);
                //IEnumerable<Employee> employees = context.Employees.Where(e=>e.Projects.Count(p=>p.StartDate>=startDate && p.EndDate<=endDate)>0).Take(30);
                //foreach (var employee in employees)
                //{
                //    Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.Manager.FirstName}");
                //    foreach (var project in employee.Projects)
                //    {
                //        Console.WriteLine($"--{project.Name} {string.Format("{0:M/d/yyyy h:mm:ss tt}", project.StartDate)} {string.Format("{0:M/d/yyyy h:mm:ss tt}", project.EndDate)}");
                //    }
                //}

                ////9.	Addresses by town name 
                //
                //IEnumerable<Address> addresses = context.Addresses
                //    .Where(a=>a.Employees.Count(e=>e.AddressID==a.AddressID)>0)
                //    .OrderByDescending(a=>a.Employees.Count)
                //    .ThenBy(a=>a.Town.Name)
                //    .Take(10);
                //
                //foreach (var address in addresses)
                //{
                //    Console.WriteLine($"{address.AddressText}, {address.Town.Name} - {address.Employees.Count} employees");
                //}

                ////10.	Employee with id 147 sorted by project names 
                //
                //Employee employee = context.Employees.Find(147);
                //Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");
                //foreach (var project in employee.Projects.OrderBy(p=>p.Name))
                //{
                //    Console.WriteLine($"{project.Name}");
                //}

                ////11.	Departments with more than 5 employees
                //
                //IEnumerable<Department> departments = context.Departments
                //    .Where(d=>d.Employees.Count>5)
                //    .OrderBy(d=>d.Employees.Count);
                //foreach (var department in departments)
                //{
                //    var manager = context.Employees
                //        .FirstOrDefault(e => e.EmployeeID == department.ManagerID);
                //    Console.WriteLine($"{department.Name} {manager.FirstName}");
                //    foreach (var employee in department.Employees)
                //    {
                //        Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");
                //    }
                //}

                ////15.	Find Latest 10 Projects
                //
                //IEnumerable<Project> projects = context.Projects.OrderByDescending(p => p.StartDate).Take(10);
                //foreach (var project in projects)
                //{
                //    Console.WriteLine($"{project.Name} {project.Description} {string.Format("{0:M/d/yyyy h:mm:ss tt}", project.StartDate)} {string.Format("{0:M/d/yyyy h:mm:ss tt}", project.EndDate)}");
                //}

                ////16.	Increase Salaries
                //
                //IEnumerable<Employee> employees = context.Employees
                //    .Where(e => e.Department.Name == "Engineering" || e.Department.Name == "Tool Design" || e.Department.Name == "Marketing" || e.Department.Name == "Information Services");
                //foreach (var employee in employees)
                //{
                //    employee.Salary = employee.Salary * (decimal)1.12;
                //    Console.WriteLine($"{employee.FirstName} {employee.LastName} (${employee.Salary})");
                //}
                //context.SaveChanges();

                ////18.	Find Employees by First Name starting with ‘SA’
                //
                IEnumerable<Employee> employees = context.Employees.Where(e => e.FirstName.ToUpper().Substring(0, 2) == "SA");
                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary})");
                }
            }
        }
    }
}
