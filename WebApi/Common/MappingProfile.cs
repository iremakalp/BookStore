using AutoMapper;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Entities;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;

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
          //7CreateMap<Book,BookDetailViewModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>src.Genre.Name));
          
        
          //CreateMap<Book,BooksViewModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>src.Genre.Name));
          //(dest=>dest.Author,opt=>opt.MapFrom(src=>src.Author.Name)
          CreateMap<Book, BooksViewModel>().ForMember(dest=>dest.Genre, 
                opt => opt.MapFrom(src=>src.Genre.Name)).ForMember(dest => dest.Author,
                opt => opt.MapFrom(src => src.Author.FirstName +" "+ src.Author.LastName));

            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Author,
                opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName));

            // Genre
          CreateMap<Genre,GenreViewModel>();
          CreateMap<CreateGenreModel,Genre>();
          CreateMap<Genre,GenreDetailViewModel>();
          

          // Author
          CreateMap<Author,AuthorViewModel>();
          CreateMap<Author,AuthorDetailViewModel>();
          CreateMap<CreateAuthorModel,Author>();
        }
    }
}
