using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;


        public CreateBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
       // fact attribute
       // fact metodun test oldugunu belirtir
        [Fact]
        public void WhenAlreadyExistBookTitleGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange -- hazirlik
            var book= new Book(){
                Title="Test Book",
                PageCount=100,
                PublishDate=new System.DateTime(2000,1,1),
                GenreId=1,
                AuthorId=1
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model= new CreateBookModel(){
                Title=book.Title
            };
            // act -- işlem(calistirma)
            // assert -- dogrulama
            // Invoking -- cagirma
            FluentActions
            .Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut");
                
            
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            // arrange
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            CreateBookModel model = new CreateBookModel(){
                Title="Hobbit",
                PageCount=100,
                PublishDate=new System.DateTime(2000,1,1),
                GenreId=1,
                AuthorId=1
            };
            command.Model=model;
      
            // act
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            // assert
            var book = _context.Books.SingleOrDefault(book=>book.Title==model.Title);
            book.Should().NotBeNull(); // book null olmamali
            book.Title.Should().Be(model.Title); // book title model title ile ayni olmali
            book.PageCount.Should().Be(model.PageCount); // book pagecount model pagecount ile ayni olmali
            book.PublishDate.Should().Be(model.PublishDate.Date); // book publishdate model publishdate ile ayni olmali
            book.GenreId.Should().Be(model.GenreId); // book genreid model genreid ile ayni olmali
            book.AuthorId.Should().Be(model.AuthorId); // book authorid model authorid ile ayni olmali

        }


    }
}