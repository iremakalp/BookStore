using System;
using System.Linq;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData(0," ")]
        [InlineData(1," ")]
        [InlineData(1,"Fa")]
        [InlineData(0,"Ro")]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId,string name)
        {
            // arrange
            UpdateGenreCommand command= new UpdateGenreCommand(null);
            command.GenreId = genreId;
            command.Model = new UpdateGenreModel(){
                Name = name
            };
            // act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            // arrange
            UpdateGenreCommand command= new UpdateGenreCommand(null);
            command.GenreId = 1;
            command.Model = new UpdateGenreModel(){
                Name = "Test"
            };
            // act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().Equals(0);
        }
    }
}