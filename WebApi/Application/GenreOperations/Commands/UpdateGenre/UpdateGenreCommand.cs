using AutoMapper;
using WebApi.DbOperations;
using System.Linq;
using System;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand    
    {
        private readonly IBookStoreDbContext _dbContext;
        public UpdateGenreModel Model { get; set; }

        public int GenreId { get; set; }

        public UpdateGenreCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var updatedGenre=_dbContext.Genres.SingleOrDefault(x=>x.Id==GenreId);
            if(updatedGenre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı");

            if(_dbContext.Genres.Any(x=>x.Name.ToLower()==Model.Name.ToLower() && x.Id!=GenreId))
                throw new InvalidOperationException("Aynı isimde kitap türü zaten mevcut");           
            
            updatedGenre.Name=Model.Name.Trim() != default ? Model.Name.Trim() : updatedGenre.Name.Trim();
            updatedGenre.IsActive=Model.IsActive;

            _dbContext.SaveChanges();
        }
    }


    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }=true;

    }
}