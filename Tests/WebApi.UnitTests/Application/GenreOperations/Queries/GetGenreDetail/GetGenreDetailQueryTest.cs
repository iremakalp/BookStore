using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGenreNotFound_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            GetGenreDetailQuery genreDetailQuery = new GetGenreDetailQuery(_context,_mapper);
            genreDetailQuery.GenreId = 999;
            // act -- işlem(calistirma)
            // assert -- dogrulama
            // Invoking -- cagirma
            FluentActions
            .Invoking(()=>genreDetailQuery.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı.");
        }

        // Happy Path
        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeReturn()
        {
            // arrange
            GetGenreDetailQuery genreDetailQuery = new GetGenreDetailQuery(_context,_mapper);
            genreDetailQuery.GenreId =2;
            // act -- işlem(calistirma)
            FluentActions.Invoking(()=>genreDetailQuery.Handle()).Invoke();

            // assert -- dogrulama
            var genre = _context.Genres.SingleOrDefault(genre=>genre.Id==genreDetailQuery.GenreId);
            genre.Should().NotBeNull();

            
        }
    }
}