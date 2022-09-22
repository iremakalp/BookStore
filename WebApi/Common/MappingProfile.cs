using AutoMapper;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Entities;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;

namespace WebApi.Common
{

    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // ilk yer source, ikinci yer target
            //CreateBookModel -->source
            //Book -->target
          
          CreateMap<CreateBookModel,Book>();
          // formember ile source dan target a giderken hangi property nin hangi property ye eşitleneceğini belirtiyoruz
          CreateMap<Book,BookDetailViewModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>((GenreEnum)src.Id).ToString()));
        
          CreateMap<Book,BooksViewModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
        
            // Genre
          CreateMap<Genre,GenreViewModel>();
          CreateMap<CreateGenreModel,Genre>();
          CreateMap<Genre,GenreDetailViewModel>();
        }
    }
}
