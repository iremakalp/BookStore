using System;
using System.Linq;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class UpdateBookCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        
        [Fact]
        public void WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 0;
            // act -- işlem(calistirma)
            // assert -- dogrulama
            // Invoking -- cagirma
            FluentActions
            .Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı");
        }

        // happy path
        [Fact]

        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            // arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 1;
            UpdateBookModel model = new UpdateBookModel(){Title = "Lord of Rings", GenreId = 1};
            command.Model = model;
           
            //Act;
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert => dogrulama;
            var book = _context.Books.SingleOrDefault(x => x.Id == command.BookId);
            book.Should().NotBeNull();
            book.Title.Should().Be(model.Title);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}