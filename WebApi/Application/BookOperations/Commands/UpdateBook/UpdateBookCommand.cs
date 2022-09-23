using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    
    public class UpdateBookCommand
    {
        public UpdateBookModel Model{get;set;}
        public int BookId{get;set;}
        private readonly IBookStoreDbContext _dbContext;
        public UpdateBookCommand(IBookStoreDbContext dbContext)
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