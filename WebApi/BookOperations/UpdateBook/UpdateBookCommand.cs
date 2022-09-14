using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperations.UpdateBook
{
    
    public class UpdateBookCommand
    {
        public UpdateBookModel Model{get;set;}
        public int BookId{get;set;}
        private readonly BookStoreDbContext _dbContext;
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var updatedBook = _dbContext.Books.SingleOrDefault(b => b.Id == BookId);
            if (updatedBook is null)
                throw new InvalidOperationException("Kitap bulunamadı");

            updatedBook.Title=Model.Title != default ? Model.Title : updatedBook.Title;
            updatedBook.GenreId=Model.GenreId != default ? Model.GenreId : updatedBook.GenreId;
            
            _dbContext.SaveChanges();
        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
    }
}