using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;
namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public CreateGenreModel Model { get; set; }

        public CreateGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var addedGenre = _dbContext.Genres.SingleOrDefault(x => x.Name.ToLower() == Model.Name.ToLower());
            if (addedGenre is not null)
                throw new InvalidOperationException("Kitap türü zaten mevcut");
            
            addedGenre = new Genre();
            addedGenre.Name = Model.Name;
            _dbContext.Genres.Add(addedGenre);
        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}