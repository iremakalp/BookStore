using System;
using System.Linq;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class  UpdateAuthorCommandValidatorTest:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0, "Jack", "London")]
        [InlineData(1," "," ")]
        [InlineData(0,"Jack"," ")]
        [InlineData(1,"Ja","Lo")]
        [InlineData(0,"Jac","Lon")]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId,string firstNamee,string lastNamee)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = authorId;
            command.Model = new UpdateAuthorModel()
            {
                FirstName = firstNamee,
                LastName = lastNamee
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
       
        // Happy path
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel()
            {
                FirstName = "Jack",
                LastName = "London"
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }

    }
}