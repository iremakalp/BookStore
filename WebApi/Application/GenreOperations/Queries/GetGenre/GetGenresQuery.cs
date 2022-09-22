using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Queries.GetGenre
{
    public class GetGenresQuery
    {
        private readonly BookStoreDbContext _dbContext;
<<<<<<< HEAD
        // iki model arasinda donusum yapmak icin mapper kullanilir
=======
>>>>>>> da40616ad028dcd1773971631e98ea225cdb0cb5
        private readonly IMapper _mapper;
        public GetGenresQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

<<<<<<< HEAD
        public List<GenresViewModel> Handle()
        {
            var genreList = _dbContext.Genres.Where<Genre>(x => x.IsActive == true).OrderBy(x=>x.Id);
            List<GenresViewModel> vm = _mapper.Map<List<GenresViewModel>>(genreList);
=======
        public List<GenreViewModel> Handle()
        {
            var genreList = _dbContext.Genres.Where<Genre>(x => x.IsActive == true).OrderBy(x=>x.Id);
            List<GenreViewModel> vm = _mapper.Map<List<GenreViewModel>>(genreList);
>>>>>>> da40616ad028dcd1773971631e98ea225cdb0cb5
            return vm;
        }
    }

<<<<<<< HEAD
    public class GenresViewModel
=======
    public class GenreViewModel
>>>>>>> da40616ad028dcd1773971631e98ea225cdb0cb5
    {
        public string Name { get; set; }
    }
}