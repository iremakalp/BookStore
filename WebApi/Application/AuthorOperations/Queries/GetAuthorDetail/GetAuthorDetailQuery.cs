using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using System;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly IBookStoreDbContext _context;
        public int AuthorId { get; set; }
        private readonly IMapper _mapper;
        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public AuthorDetailViewModel Handle()
        {
           var author=_context.Authors.SingleOrDefault(x=>x.Id== AuthorId);
           if(author is null)
            throw new InvalidOperationException("Yazar bulunamad─▒");
           
            AuthorDetailViewModel vm= _mapper.Map<AuthorDetailViewModel>(author);
            return vm;
        }
    }
    public class AuthorDetailViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }

    }
}