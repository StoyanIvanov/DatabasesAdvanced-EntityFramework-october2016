using System.Collections.Generic;
using System.Globalization;
using System.IO;
using BookShopSystem.Models;

namespace BookShopSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookShopSystem.Data.BookShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BookShopSystem.Data.BookShopContext";
        }

        protected override void Seed(BookShopSystem.Data.BookShopContext context)
        {

            var authors = new List<Author>();
            using (var reader = new StreamReader("authors.txt"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var names = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    authors.Add(new Author() { FirstName = names[0], LastName = names[1] });

                    line = reader.ReadLine();
                }
            }

            using (var reader = new StreamReader("categories.txt"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    context.Categories.Add(new Category() { Name = line });
                    line = reader.ReadLine();

                }
                context.SaveChanges();

            }

            using (var reader = new StreamReader("books.txt"))
            {
                var line = reader.ReadLine();
                line = reader.ReadLine();
                while (line != null)
                {
                    var data = line.Split(new[] { ' ' }, 6);
                    var authorIndex = new Random().Next(0, authors.Count);
                    var author = authors[authorIndex];
                    var edition = (BookTypes)int.Parse(data[0]);
                    var releaseDate = DateTime.ParseExact(data[1], "d/M/yyyy", CultureInfo.InvariantCulture);
                    var copies = int.Parse(data[2]);
                    var price = decimal.Parse(data[3], CultureInfo.InvariantCulture);
                    var ageRestriction = (AgeRestriction)int.Parse(data[4]);
                    var title = data[5];
                    ICollection<Category> categories = new HashSet<Category>();

                    for (int i = 0; i < 8; i++)
                    {
                        var categoryIndex = new Random().Next(1, context.Categories.Count());
                        categories.Add(context.Categories.FirstOrDefault(c=>c.Id==categoryIndex));
                    }

                    context.Books.Add(new Book()
                    {
                        Author = author,
                        Edition = edition,
                        ReleaseDate = releaseDate,
                        Copies = copies,
                        Price = price,
                        AgeRestriction = ageRestriction,
                        Title = title,
                        Categories = categories
                    });

                    line = reader.ReadLine();
                }
            }
        }
    }
}
