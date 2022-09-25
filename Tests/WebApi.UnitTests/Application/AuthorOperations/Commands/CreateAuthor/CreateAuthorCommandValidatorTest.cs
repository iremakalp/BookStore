using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTest:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("","")]
        [InlineData("K","ART")]
        [InlineData("KART","")]
        [InlineData("KART","ART")]

        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string firstName,string lastName)
        {
           // Arrange -- hazirlik
           CreateAuthorCommand command = new CreateAuthorCommand(null,null);
              command.Model = new CreateAuthorModel(){
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = DateTime.Now.Date.AddYears(-40)
              };
            // Act -- calistirma
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result= validator.Validate(command);
            // Assert -- dogrulama
            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Fact]

        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReeturnError()
        {
            // Arrange -- hazirlik
            CreateAuthorCommand command= new CreateAuthorCommand(null,null);
            command.Model = new CreateAuthorModel(){
                FirstName = "Test Firstname",
                LastName = "Test Lastname",
                DateOfBirth = DateTime.Now.Date
            };
            // Act -- calistirma
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);
            // Assert -- dogrulama
            result.Errors.Count().Should().BeGreaterThan(0);

        }

        // Happy Path
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange -- hazirlik
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
            command.Model= new CreateAuthorModel{
                FirstName = "Test Firstname",
                LastName = "Test Lastname",
                DateOfBirth = DateTime.Now.Date.AddYears(-40)
            };
            // Act -- calistirma
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result= validator.Validate(command);
            // Assert -- dogrulama
            result.Errors.Count().Should().Equals(0);
        }
    }
}