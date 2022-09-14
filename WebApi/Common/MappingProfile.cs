    using AutoMapper;
using WebApi.BookOperations.CreateBook;

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
        }
    }
}
