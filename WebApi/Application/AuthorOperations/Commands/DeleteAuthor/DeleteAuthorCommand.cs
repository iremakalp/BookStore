using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly BookStoreDbContext _context;
        public int AuthorId { get; set; }
        public DeleteAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Yazar bulunamadı.");
            if(_context.Books.Any(x => x.AuthorId == AuthorId))
                throw new InvalidOperationException("Bu yazarın kitabı olduğu için silinemez.");
            
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}