using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly BookStoreDbContext _context;
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }
        public UpdateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        
        public void Handle()
        {
            var updatedAuthor=_context.Authors.SingleOrDefault(a => a.Id == AuthorId);
            if (updatedAuthor is null)
                throw new InvalidOperationException("Yazar bulunamadı");
                
            if(_context.Authors.Any(x=>x.FirstName.ToLower()==Model.FirstName.ToLower() && x.LastName.ToLower()==Model.LastName.ToLower() && x.Id!=AuthorId))
                throw new InvalidOperationException("Aynı isimde yazar zaten mevcut");

            updatedAuthor.FirstName = Model.FirstName != default ? Model.FirstName : updatedAuthor.FirstName;
            updatedAuthor.LastName = Model.LastName != default ? Model.LastName : updatedAuthor.LastName;

            _context.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}