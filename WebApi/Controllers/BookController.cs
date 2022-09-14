using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
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
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result= query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                query.BookId=id;
                result=query.Handle();
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            return Ok(result);
            
        }
        //[HttpGet]
        //public Book Get([FromQuery] string id)
        //{
        //    var book = _context.Books.Where(b => b.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //}

        [HttpPost]
        public IActionResult Add([FromBody] CreateBookModel book)
        {
            CreateBookCommand command = new CreateBookCommand(_context);

            try
            {
                command.Model=book;
                command.Handle();
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook([FromBody] UpdateBookModel book,int id)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.BookId=id;
                command.Model=book;
                command.Handle();
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            try
            {
                command.BookId=id;
                command.Handle();
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}