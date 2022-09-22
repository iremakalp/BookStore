using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

using WebApi.DbOperations;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Queries.GetGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
<<<<<<< HEAD
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
=======
>>>>>>> da40616ad028dcd1773971631e98ea225cdb0cb5

namespace WebApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]

    public class GenreController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
<<<<<<< HEAD
        public ActionResult GetGenres()
=======
        public IActionResult GetGenres()
>>>>>>> da40616ad028dcd1773971631e98ea225cdb0cb5
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }
<<<<<<< HEAD
        
        [HttpGet("{id}")]
        public ActionResult GetGenresDetail(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = id;
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel genre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
=======

        [HttpPut]
        public IActionResult Add([FromBody] CreateGenreModel genre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
>>>>>>> da40616ad028dcd1773971631e98ea225cdb0cb5
            command.Model = genre;
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

<<<<<<< HEAD
        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel genre)
=======
        [HttpPost("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateGenreModel genre)
>>>>>>> da40616ad028dcd1773971631e98ea225cdb0cb5
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.Model = genre;
            command.GenreId = id;
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            
            return Ok();
        }
        [HttpDelete("{id}")]
<<<<<<< HEAD
        public IActionResult DeleteGenre(int id)
=======
        public IActionResult Delete(int id)
>>>>>>> da40616ad028dcd1773971631e98ea225cdb0cb5
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}