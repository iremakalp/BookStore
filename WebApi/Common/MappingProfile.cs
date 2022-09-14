    using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;

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
          CreateMap<Book,BookDetailViewModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
        
          CreateMap<Book,BooksViewModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
        
        }
    }
}
