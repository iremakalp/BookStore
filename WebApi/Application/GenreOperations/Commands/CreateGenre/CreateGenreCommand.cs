using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;
namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public CreateGenreModel Model { get; set; }
        private readonly IMapper _mapper;

        public CreateGenreCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var addedGenre = _dbContext.Genres.SingleOrDefault(x => x.Name.ToLower() == Model.Name.ToLower());
            if (addedGenre is not null)
                throw new InvalidOperationException("Kitap türü zaten mevcut");  
            addedGenre = _mapper.Map<Genre>(Model);
            _dbContext.Genres.Add(addedGenre);
            _dbContext.SaveChanges();

        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}