using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace   WebApi.UnitTests.TestSetup
{
    public static class Books
    {
        public static void GetBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                new Book
            {
                Title = "Lean Startup",
                GenreId = 1,
                AuthorId = 1,
                PageCount = 200,
                PublishDate = new DateTime(2001, 06, 12)
            },
            new Book
            {
                Title = "Herland",
                GenreId = 2,
                AuthorId = 2,
                PageCount = 250,
                PublishDate = new DateTime(1972, 11, 01)
            },
            new Book
            {
                Title = "Dune",
                GenreId = 2,
                AuthorId = 3,
                PageCount = 540,
                PublishDate = new DateTime(1965, 06, 01)
            },
            new Book
            {
                Title = "I, Robot",
                GenreId = 2,
                AuthorId = 4,
                PageCount = 400,
                PublishDate = new DateTime(1950, 01, 01)
            },
            new Book
            {
                Title = "The Hitchhiker's Guide to the Galaxy",
                GenreId = 2,
                AuthorId = 1,
                PageCount = 140,
                PublishDate = new DateTime(1979, 09, 12)
            });
            context.SaveChanges();
        }
    }
}