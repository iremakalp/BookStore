using System;
using System.Linq;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class   UpdateAuthorCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            UpdateAuthorCommand command= new UpdateAuthorCommand(_context);
            command.AuthorId=0;

            // act -- işlem(calistirma)
            // assert -- dogrulama
            // Invoking -- cagirma
            FluentActions.Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı");

        }


        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdate()
        {
            // arrange -- hazirlik
            UpdateAuthorCommand command= new UpdateAuthorCommand(_context);
            command.AuthorId=1;
            command.Model = new UpdateAuthorModel()
            {
                FirstName = "Updated FirstName",
                LastName = "Updated LastName",
            };

            // act -- işlem(calistirma)
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            // assert -- dogrulama
            var author = _context.Authors.SingleOrDefault(author=>author.Id==command.AuthorId);
            author.FirstName.Should().Be(command.Model.FirstName);
            author.LastName.Should().Be(command.Model.LastName);

        }
    }

}