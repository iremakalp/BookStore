using System;
using System.Linq;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class DeleteBookCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 1000;
            // act -- işlem(calistirma)
            // assert -- dogrulama
            // Invoking -- cagirma
            FluentActions
            .Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı");
        }

        // happy path
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeDeleted()
        {
            // arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 1;
            // act -- işlem(calistirma)
            FluentActions.Invoking(()=>command.Handle()).Invoke();
            // assert -- dogrulama
            var book = _context.Books.SingleOrDefault(x=>x.Id == command.BookId);
            book.Should().BeNull();

        }
    }
}