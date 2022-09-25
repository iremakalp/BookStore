using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthor_InvalidOperationException_ShouldBeReturn()
        {
            // arrange -- hazirlama
            var author= new Author(){
                FirstName="Test Author",
                LastName="Test Surname",
                DateOfBirth=new System.DateTime(1970,1,1)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();
            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            command.Model= new CreateAuthorModel(){
                FirstName=author.FirstName,
                LastName=author.LastName,
                DateOfBirth=author.DateOfBirth
            };
          
            // act -- işlem(calistirma)
            // assert -- dogrulama
            FluentActions.
            Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            // arrange -- hazirlik
            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            CreateAuthorModel model= new CreateAuthorModel
            {
                FirstName = "Test",
                LastName = "Test",
                DateOfBirth = System.DateTime.Now.Date.AddYears(-30)
            };
            command.Model=model;

            // act -- işlem(calistirma)
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            // assert -- dogrulama
            var author = _context.Authors.SingleOrDefault(x=>x.FirstName==model.FirstName && x.LastName==model.LastName);       
            author.Should().NotBeNull();
            author.FirstName.Should().Be(model.FirstName);
            author.LastName.Should().Be(model.LastName);
            author.DateOfBirth.Should().Be(model.DateOfBirth.Date);

        }
    }
}