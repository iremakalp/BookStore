using System;
using System.Linq;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
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
            command.BookId = 4;
            command.Model= new UpdateBookModel()
            {
                Title = "Updated Title",
                GenreId = 2
            };
           
            //Act;
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert => dogrulama;
            var book = _context.Books.SingleOrDefault(x => x.Id == command.BookId);
            book.Title.Should().Be(command.Model.Title);
            book.GenreId.Should().Be(command.Model.GenreId);
            
        }
    }
}