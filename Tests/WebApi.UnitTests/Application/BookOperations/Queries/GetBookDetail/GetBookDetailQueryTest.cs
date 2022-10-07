using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
       
        [Fact]
        public void WhenBookNotFound_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            GetBookDetailQuery bookDetailQuery = new GetBookDetailQuery(_context,_mapper);
            bookDetailQuery.BookId = 999;
            // act -- işlem(calistirma)
            // assert -- dogrulama
            // Invoking -- cagirma
            FluentActions
            .Invoking(()=>bookDetailQuery.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı");
        }

        // Happy Path
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeReturn()
        {
            // arrange
            GetBookDetailQuery bookDetailQuery = new GetBookDetailQuery(_context,_mapper);
            bookDetailQuery.BookId =4;
            // act -- işlem(calistirma)
            FluentActions.Invoking(()=>bookDetailQuery.Handle()).Invoke();
            // assert -- dogrulama
            var book = _context.Books.SingleOrDefault(book=>book.Id==bookDetailQuery.BookId);
            book.Should().NotBeNull();

            
        }
    }
}