using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]

    public class BookController:ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(b => b.Id).ToList();
            return bookList;
        }
        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = _context.Books.Where(b => b.Id == id).SingleOrDefault();
            return book;
        }
        //[HttpGet]
        //public Book Get([FromQuery] string id)
        //{
        //    var book = _context.Books.Where(b => b.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //}

        [HttpPost]
        public IActionResult Add([FromBody] Book book)
        {
            var addedBook = _context.Books.SingleOrDefault(b => b.Title == book.Title);
            if (addedBook is not null) // addedBook!=null
                return BadRequest();
            
            _context.Books.Add(book);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook([FromBody] Book book,int id)
        {
            var updatedBook = _context.Books.SingleOrDefault(b => b.Id == id);
            if (updatedBook is null)
                return BadRequest();

            updatedBook.GenreId=book.GenreId!=default?book.GenreId:updatedBook.GenreId;
            updatedBook.Title = book.Title!=default?book.Title:updatedBook.Title;
            updatedBook.PageCount = book.PageCount!=default?book.PageCount:updatedBook.PageCount;   
            updatedBook.PublishDate = book.PublishDate!=default?book.PublishDate:updatedBook.PublishDate;
            _context.SaveChanges();

            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var deletedBook = _context.Books.SingleOrDefault(b => b.Id == id);
            if (deletedBook is null)
                return BadRequest();

            _context.Books.Remove(deletedBook);
                        _context.SaveChanges();

            return Ok();
        }
    }
}