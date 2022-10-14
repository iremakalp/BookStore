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
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;


namespace WebApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]

    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public ActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        
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
            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper);
            command.Model = genre;
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel genre)
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
        public IActionResult DeleteGenre(int id)
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