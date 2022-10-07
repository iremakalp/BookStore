using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void GetAuthors(this BookStoreDbContext context)
        {
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
                },
                new Author
                {
                    FirstName = "J. R. R.",
                    LastName = "Tolkien",
                    DateOfBirth = new DateTime(1892, 1, 3)
                },
                new Author
                {
                    FirstName = "Isaac",
                    LastName = "Asimov",
                    DateOfBirth = new DateTime(1920, 1, 2)
                });
            context.SaveChanges();
        }
    }
}