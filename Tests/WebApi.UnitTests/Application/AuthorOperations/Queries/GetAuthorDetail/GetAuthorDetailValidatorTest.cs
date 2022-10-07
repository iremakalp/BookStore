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
    public class GetAuthorDetailValidatorTest:IClassFixture<CommonTestFixture>
    { 
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId)
        {
            // arrange
            GetAuthorDetailQuery authorDetailQuery = new GetAuthorDetailQuery(null,null);
            authorDetailQuery.AuthorId = authorId;
            // act -- işlem(calistirma)
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(authorDetailQuery);
            // assert -- dogrulama
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            // arrange
            GetAuthorDetailQuery authorDetailQuery = new GetAuthorDetailQuery(null,null);
            authorDetailQuery.AuthorId = 1;
            // act -- işlem(calistirma)
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(authorDetailQuery);
            // assert -- dogrulama
            result.Errors.Count.Should().Equals(0);
        }
    }
}