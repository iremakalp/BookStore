using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTest:IClassFixture<CommonTestFixture>
    {
        
        [Theory]
        [InlineData("")]
        [InlineData("R")]
        [InlineData("Rom")]
        [InlineData("Ro")]
        [InlineData(" ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name)
        {
            // Arrange
            CreateGenreCommand command = new CreateGenreCommand(null,null);
            command.Model = new CreateGenreModel(){
                Name = name
            };

            // Act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count().Should().BeGreaterThan(0);
        }

        // Happy Path
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            CreateGenreCommand command = new CreateGenreCommand(null,null);
            command.Model = new CreateGenreModel(){
                Name = "Test"
            };

            // Act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count().Should().Equals(0);
        }
    }
}