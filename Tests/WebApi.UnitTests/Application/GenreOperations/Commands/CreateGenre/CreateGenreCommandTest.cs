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

        public CreateGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]

        public void WhenAlreadyExistGenre_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            var genre= new Genre(){
                Name="Test Genre"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper);
            command.Model= new CreateGenreModel(){
                Name=genre.Name
            };
            // Act - Assert
            FluentActions.Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Kitap türü zaten mevcut");
            
        }

        // Happy Path
        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            // Arrange
            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper);
            command.Model = new CreateGenreModel(){
                Name = "Test"
            };

            // Act
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            // Assert
            var genre = _context.Genres.SingleOrDefault(genre=>genre.Name == command.Model.Name);
            genre.Should().NotBeNull();
        }
    }
}