using System;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTest:IClassFixture<CommonTestFixture>
    {
        // Theory: Testlerinizi birbirinden bağımsız olarak çalıştırmanızı sağlar.
        // InlineData: Testlerinize parametre göndermenizi sağlar.
        // Her bir inline data icin bir test calisir
        [Theory]
 
        [InlineData("Lord Of The Rings",0,1,0)]
        [InlineData("Lord Of The Rings",0,1,1)]
        [InlineData("Lord Of The Rings",100,1,0)]
        [InlineData("Lor",0,1,1)]
        [InlineData("Lord",100,1,0)]
        [InlineData("Lord",100,0,0)]
        [InlineData("Lord",0,1,0)]
        [InlineData(" ",100,1,0)]
        [InlineData(" ",100,1,1)]
        [InlineData("",100,1,1)]

        // Bu testi calistirinca hata verir cunku bu testteki tum validasyonlar dogru
        // [InlineData("Lord of the rings",100,1,1)] 
        
        public void WhenInvalidInputsAreGive_Validator_ShouldBeReturnErrors(string title,int pageCount,int genreId,int authorId)
        {
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel(){
                Title=title,
                PageCount=pageCount,
                GenreId=genreId,
                AuthorId=authorId,
                PublishDate=DateTime.Now.Date.AddYears(-1),
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReeturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel(){
                Title="Lord Of The Rings",
                PageCount=100,
                GenreId=1,
                PublishDate=DateTime.Now.Date,
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

         // happy path
        // testin basarili oldugu durum
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel(){
                Title="Lord Of The Rings",
                PageCount=100,
                GenreId=1,
                PublishDate=DateTime.Now.Date.AddYears(-2),
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}
