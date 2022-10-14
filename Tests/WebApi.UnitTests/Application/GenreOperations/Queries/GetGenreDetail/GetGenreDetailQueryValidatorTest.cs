using System;
using System.Linq;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTest:IClassFixture<CommonTestFixture>
    {
         [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId)
        {
            // arrange
            GetGenreDetailQuery genreDetailQuery = new GetGenreDetailQuery(null,null);
            genreDetailQuery.GenreId = genreId;
            // act -- işlem(calistirma)
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(genreDetailQuery);
            // assert -- dogrulama
            result.Errors.Count.Should().BeGreaterThan(0); 
        }

        // Happy Path
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            // arrange
            GetGenreDetailQuery genreDetailQuery = new GetGenreDetailQuery(null,null);
            genreDetailQuery.GenreId = 1;
            // act -- işlem(calistirma)
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(genreDetailQuery);
            // assert -- dogrulama
            result.Errors.Count.Should().Equals(0);

        }
    }
}