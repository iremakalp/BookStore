using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Genres.AddRange(
                    new Genre 
                    { 
                        Name = "Personel Growth" 
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }
                );
                context.Authors.AddRange( 
                    new Author
                    {
                        FirstName = "Stephen",
                        LastName = "King",
                        DateOfBirth = new DateTime(1947, 9, 21)
                    },
                    new Author
                    {
                        FirstName = "Joanne",
                        LastName = "Rowling",
                        DateOfBirth = new DateTime(1965, 7, 31)
                    },
                    new Author
                    {
                        FirstName = "George",
                        LastName = "Martin",
                        DateOfBirth = new DateTime(1948, 9, 20)
                    }                   
                );
                context.Books.AddRange(
                    new Book
                    {
                        Title = "Lean Startup",
                        GenreId = 1,
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 06, 12)
                    },
                    new Book
                    {
                        Title = "Herland",
                        GenreId = 2,
                        PageCount = 250,
                        PublishDate = new DateTime(1972, 11, 01)
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 2,
                        PageCount = 540,
                        PublishDate = new DateTime(1965, 06, 01)
                    }
                );
                
                
                

                context.SaveChanges();
            }
        }
    }
}