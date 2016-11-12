using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using BookShopSystem.Data;
using BookShopSystem.Models;

namespace BookShopSystem.ConsoleClient
{
    class Startup
    {
        static void Main()
        {
            var context = new BookShopContext();
            var migrationStrategy = new DropCreateDatabaseIfModelChanges<BookShopContext>();
            Database.SetInitializer(migrationStrategy);
            
            //Inicialize database
            context.Database.Initialize(true);

            //Get all books after the year 2000. Print only their titles.
            //using (context)
            //{
            //    ICollection<Book> books = context.Books.Where(book => book.ReleaseDate.Value.Year > 2000).ToArray();
            //    foreach (var book in books)
            //    {
            //        Console.WriteLine(book.Title);
            //    }
            //}

            //Get all authors with at least one book with release date before 1990.Print their first name and last name.
            //using (context)
            //{
            //    var authors = context.Authors.Where(a=>a.Books.Count(b=>b.ReleaseDate.Value.Year<1990)>0).Select(a=>new {a.FirstName, a.LastName}).ToList();
            //    foreach (var author in authors)
            //    {
            //        Console.WriteLine($"{author.FirstName} {author.LastName}");
            //    }
            //}

            //Get all authors, ordered by the number of their books (descending). Print their first name, last name and 
            //book count.
            //using (context)
            //{
            //    var authors =
            //        context.Authors.Select(a => new {a.FirstName, a.LastName, a.Books.Count})
            //            .OrderByDescending(a => a.Count);
            //    foreach (var author in authors)
            //    {
            //        Console.WriteLine($"{author.FirstName} {author.LastName} {author.Count}");
            //    }
            //}

            //Get all books from author George Powell, ordered by their release date (descending), then by book title
            //(ascending).Print the book's title, release date and copies.
            using (context)
            {
                ICollection<Book> books =
                    context.Books
                        .Where(b => b.Author.FirstName + " " + b.Author.LastName == "George Powell")
                        .OrderByDescending(b => b.ReleaseDate)
                        .ThenBy(b => b.Title)
                        .ToList();

                foreach (var book in books)
                {
                    Console.WriteLine($"{book.Title} {book.ReleaseDate} {book.Copies}");
                }
            }


        }
    }
}
