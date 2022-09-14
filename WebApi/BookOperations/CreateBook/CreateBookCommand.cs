using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.CreateBook
{
    
    public class CreateBookCommand
    {
        public CreateBookModel Model{get;set;}
        private readonly BookStoreDbContext _dbContext;
        public CreateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public void Handle()
        {
            var addedBook = _dbContext.Books.SingleOrDefault(b => b.Title == Model.Title);
            if (addedBook is not null) // addedBook!=null
                throw new InvalidOperationException("Kitap zaten mevcut");
            
            addedBook= new Book();
            addedBook.Title=Model.Title;
            addedBook.PageCount=Model.PageCount;
            addedBook.PublishDate=Model.PublishDate;
            addedBook.GenreId=Model.GenreId;
            _dbContext.Books.Add(addedBook);
            _dbContext.SaveChanges();
        }
    }
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreId { get; set; }
    }
}

  