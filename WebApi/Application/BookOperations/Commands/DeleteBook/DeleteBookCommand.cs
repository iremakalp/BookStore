using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
       private readonly IBookStoreDbContext _dbContext;
       public int BookId{get;set;}
        public DeleteBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var deletedBook = _dbContext.Books.SingleOrDefault(b => b.Id == BookId);
            if (deletedBook is null)
                throw new InvalidOperationException("Kitap bulunamad─▒");
            _dbContext.Books.Remove(deletedBook);
            _dbContext.SaveChanges();
        }
    }
}