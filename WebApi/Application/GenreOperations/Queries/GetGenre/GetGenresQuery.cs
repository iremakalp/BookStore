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
        // iki model arasinda donusum yapmak icin mapper kullanilir
        private readonly IMapper _mapper;
        public GetGenresQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genreList = _dbContext.Genres.Where<Genre>(x => x.IsActive == true).OrderBy(x=>x.Id);
            List<GenresViewModel> vm = _mapper.Map<List<GenresViewModel>>(genreList);
            return vm;
        }
    }

    public class GenresViewModel
    {
        public string Name { get; set; }
    }
}