using System;
using System.Linq;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange -- hazirlik
            DeleteAuthorCommand command= new DeleteAuthorCommand(_context);
            command.AuthorId=0;

            // Act -- işlem(calistirma)
            // Assert -- dogrulama
            // Invoking -- cagirma
            FluentActions.Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı");
            
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeDeleted()
        {
            // Arrange -- hazirlik
            DeleteAuthorCommand command= new DeleteAuthorCommand(_context);
            command.AuthorId=1;

            // Act -- işlem(calistirma)
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            // Assert -- dogrulama
            var author = _context.Authors.SingleOrDefault(x=>x.Id == command.AuthorId);
            author.Should().BeNull();
            

        }
    }
}