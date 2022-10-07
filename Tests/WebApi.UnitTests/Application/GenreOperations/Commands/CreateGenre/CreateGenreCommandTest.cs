using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTest(CommonTestFixture testFixture, IMapper mapper)
        {
            _context = testFixture.Context;
            _mapper = mapper;
        }

        [Fact]

        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper);
            command.Model = new CreateGenreModel(){
                Name = "Romance"
            };

            // Act - Assert
            FluentActions.Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Kitap türü zaten mevcut");
            
        }
    }
}