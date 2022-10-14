using System;
using System.Linq;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            UpdateGenreCommand command= new UpdateGenreCommand(_context);
            command.GenreId = 0;
            command.Model = new UpdateGenreModel(){
                Name = "Test Genre"
            };
            if(_context.Genres.Any(x=>x.Id==command.GenreId))
            {
                // Act
                FluentActions.Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı");
            }
            if(_context.Genres.Any(x=>x.Name.ToLower()==command.Model.Name.ToLower() && x.Id!=command.GenreId))
            {
                // Act
                FluentActions.Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isimde kitap türü zaten mevcut");
            }


        }

        // Happy Path

        [Fact]
        public void WhenValidIsGiven_Genre_ShouldBeUpdated()
        {
            // Arrange
            UpdateGenreCommand command= new UpdateGenreCommand(_context);
            command.GenreId = 1;
            command.Model = new UpdateGenreModel(){
                Name = "Test"
            };

            // Act
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            //Asser
            var genre=_context.Genres.SingleOrDefault(genre=>genre.Id==command.GenreId);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(command.Model.Name);


        }
    }
}