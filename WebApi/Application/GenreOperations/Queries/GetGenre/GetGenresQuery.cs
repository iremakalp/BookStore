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
        private readonly IMapper _mapper;
        public GetGenresQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<GenreViewModel> Handle()
        {
            var genreList = _dbContext.Genres.Where<Genre>(x => x.IsActive == true).OrderBy(x=>x.Id);
            List<GenreViewModel> vm = _mapper.Map<List<GenreViewModel>>(genreList);
            return vm;
        }
    }

    public class GenreViewModel
    {
        public string Name { get; set; }
    }
}