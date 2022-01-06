using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BooksApi.Models;
using System;
using System.Linq;

namespace BooksApi.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BooksContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BooksContext>>()))
            {
                // Look for any books.
                if (context.BooksItems.Any())
                {
                    return;   // DB has been seeded
                }
                context.BooksItems.AddRange(
                    new BooksItem
                    {
                        Title = "The Hobbit",
                        Author = "J. R. R. Tolkien",
                        Genre = "Fantasy",
                        IsAvailable = true,
                    },
                    new BooksItem
                    {
                        Title = "Harry Potter and the Philosopher's Stone",
                        Author = "J. K. Rowling",
                        Genre = "Fantasy",
                        IsAvailable = true,
                    },
                    new BooksItem
                    {
                        Title = "And Then There Were None",
                        Author = "Agatha Christie",
                        Genre = "Mystery",
                        IsAvailable = true,
                    },
                    new BooksItem
                    {
                        Title = "The Da Vinci Code",
                        Author = "Dan Brown",
                        Genre = "Mystery thriller",
                        IsAvailable = true,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}