using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
       private readonly BookStoreDbContext _dbContext;
       public int BookId{get;set;}
        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var deletedBook = _dbContext.Books.SingleOrDefault(b => b.Id == BookId);
            if (deletedBook is null)
                throw new InvalidOperationException("Kitap bulunamadı");
            _dbContext.Books.Remove(deletedBook);
            _dbContext.SaveChanges();
        }
    }
}