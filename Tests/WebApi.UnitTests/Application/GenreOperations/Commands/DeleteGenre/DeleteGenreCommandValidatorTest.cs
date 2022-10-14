using System;
using System.Linq;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTest:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId)
        {
            // arrange
            DeleteGenreCommand command= new DeleteGenreCommand(null);
            command.GenreId = genreId;
            // act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            // arrange
            DeleteGenreCommand command= new DeleteGenreCommand(null);
            command.GenreId = 1;
            // act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().Equals(0);
        }
    }
}