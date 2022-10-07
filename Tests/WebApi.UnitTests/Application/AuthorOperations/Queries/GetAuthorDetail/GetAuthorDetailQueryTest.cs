using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTest:IClassFixture<CommonTestFixture>
    { 
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAuthorNotFound_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            GetAuthorDetailQuery authorDetailQuery = new GetAuthorDetailQuery(_context,_mapper);
            authorDetailQuery.AuthorId = 999;
            // act -- işlem(calistirma)
            // assert -- dogrulama
            // Invoking -- cagirma
            FluentActions
            .Invoking(()=>authorDetailQuery.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı");
        }

        // Happy Path
        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeReturn()
        {
            // arrange
            GetAuthorDetailQuery authorDetailQuery = new GetAuthorDetailQuery(_context,_mapper);
            authorDetailQuery.AuthorId =1;
            // act -- işlem(calistirma)
            FluentActions.Invoking(()=>authorDetailQuery.Handle()).Invoke();
            // assert -- dogrulama
            var author = _context.Authors.SingleOrDefault(author=>author.Id==authorDetailQuery.AuthorId);
            author.Should().NotBeNull();

            
        }
    }
}