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
<<<<<<< HEAD
            
            if(_dbContext.Genres.Any(x=>x.Name.ToLower()==Model.Name.ToLower() && x.Id!=GenreId))
                throw new InvalidOperationException("Aynı isimde kitap türü zaten mevcut");           
            
            updatedGenre.Name=Model.Name.Trim() != default ? Model.Name.Trim() : updatedGenre.Name.Trim();
            updatedGenre.IsActive=Model.IsActive;
=======
            updatedGenre.Name=Model.Name != default ? Model.Name : updatedGenre.Name;
>>>>>>> da40616ad028dcd1773971631e98ea225cdb0cb5
            _dbContext.SaveChanges();
        }
    }


    public class UpdateGenreModel
    {
        public string Name { get; set; }
<<<<<<< HEAD
        public bool IsActive { get; set; }=true;
=======
>>>>>>> da40616ad028dcd1773971631e98ea225cdb0cb5
    }
}