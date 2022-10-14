// Namespace: WebApi.GenreOperations.DeleteGenre
//class DeleteGenreCommand
using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    { 
        private readonly IBookStoreDbContext _dbContext;
        public int GenreId{get;set;}
        
        public DeleteGenreCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var deletedGenre=_dbContext.Genres.SingleOrDefault(x=>x.Id==GenreId);
            if(deletedGenre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı");
            
            _dbContext.Genres.Remove(deletedGenre);
            _dbContext.SaveChanges();
        }
    }
}
