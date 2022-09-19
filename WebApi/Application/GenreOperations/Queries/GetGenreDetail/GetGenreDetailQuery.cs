using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int GenreId { get; set; }
        public GetGenreDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre=_dbContext.Genres.SingleOrDefault(x=>x.IsActive && x.Id==GenreId);
            if(genre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı.");       
            
            return _mapper.Map<GenreDetailViewModel>(genre);;
        }
    }

    public class GenreDetailViewModel
    {
        public string Name { get; set; }
    }
}