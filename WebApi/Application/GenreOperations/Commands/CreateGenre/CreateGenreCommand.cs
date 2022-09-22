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
<<<<<<< HEAD

        public CreateGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
=======
        private readonly IMapper _mapper;

        public CreateGenreCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
>>>>>>> da40616ad028dcd1773971631e98ea225cdb0cb5
        }
        public void Handle()
        {
            var addedGenre = _dbContext.Genres.SingleOrDefault(x => x.Name.ToLower() == Model.Name.ToLower());
            if (addedGenre is not null)
                throw new InvalidOperationException("Kitap türü zaten mevcut");
<<<<<<< HEAD
            
            addedGenre = new Genre();
            addedGenre.Name = Model.Name;
            _dbContext.Genres.Add(addedGenre);
=======
            addedGenre = new Genre();
            
            addedGenre = _mapper.Map<Genre>(Model);
            _dbContext.Genres.Add(addedGenre);
            _dbContext.SaveChanges();
>>>>>>> da40616ad028dcd1773971631e98ea225cdb0cb5
        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}