using AutoMapper;
using WebApi.DbOperations;
using System.Linq;
using System;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand    
    {
        private readonly BookStoreDbContext _dbContext;
        public UpdateGenreModel Model { get; set; }

        public int GenreId { get; set; }

        public UpdateGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var updatedGenre=_dbContext.Genres.SingleOrDefault(x=>x.Id==GenreId);
            if(updatedGenre is null)
                throw new InvalidOperationException("Kitap bulunamadı");
            updatedGenre.Name=Model.Name != default ? Model.Name : updatedGenre.Name;
            _dbContext.SaveChanges();
        }
    }


    public class UpdateGenreModel
    {
        public string Name { get; set; }
    }
}