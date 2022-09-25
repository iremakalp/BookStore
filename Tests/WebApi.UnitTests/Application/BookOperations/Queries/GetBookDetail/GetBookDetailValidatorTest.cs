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
    public class GetBookDetailValidatorTest:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId)
        {
            // arrange
            GetBookDetailQuery bookDetailQuery = new GetBookDetailQuery(null,null);
            bookDetailQuery.BookId = bookId;
            // act -- işlem(calistirma)
            GetBookDetailValidator validator = new GetBookDetailValidator();
            var result = validator.Validate(bookDetailQuery);
            // assert -- dogrulama
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        // Happy Path
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            // arrange
            GetBookDetailQuery bookDetailQuery = new GetBookDetailQuery(null,null);
            bookDetailQuery.BookId = 1;
            // act -- işlem(calistirma)
            GetBookDetailValidator validator = new GetBookDetailValidator();
            var result = validator.Validate(bookDetailQuery);
            // assert -- dogrulama
            result.Errors.Count.Should().Equals(0);
        }
    }
}