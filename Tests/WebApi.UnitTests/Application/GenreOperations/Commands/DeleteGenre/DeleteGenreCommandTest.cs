using System;
using System.Linq;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            DeleteGenreCommand command= new DeleteGenreCommand(_context);
            command.GenreId = 999;
            // Act & Assert
            FluentActions.Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Kitap türü bulunamadı");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
        {

            // arrange
            DeleteGenreCommand command= new DeleteGenreCommand(_context);
            command.GenreId = 1;
            // Act
            FluentActions.Invoking(()=>command.Handle()).Invoke();
            // Assert
            var genre = _context.Genres.SingleOrDefault(genre=>genre.Id==command.GenreId);
            genre.Should().BeNull();


        }
    }
}