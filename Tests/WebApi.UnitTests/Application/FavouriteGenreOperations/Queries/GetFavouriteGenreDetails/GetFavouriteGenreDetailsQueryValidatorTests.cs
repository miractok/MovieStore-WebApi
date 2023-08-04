using FluentAssertions;
using TestSetup;
using WebApi.Application.FavouriteGenreOperations.Queries.GetFavouriteGenreDetails;

namespace Applications.FavouriteGenreOperations.Queries.GetFavouriteGenreDetails
{
    public class GetFavouriteGenreDetailsQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            GetFavouriteGenreDetailsQuery query = new GetFavouriteGenreDetailsQuery(null, null);
            query.favouriteGenreId = 0;
            GetFavouriteGenreDetailsQueryValidator validator = new GetFavouriteGenreDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldGetDetail()
        {
            GetFavouriteGenreDetailsQuery query = new GetFavouriteGenreDetailsQuery(null, null);
            query.favouriteGenreId = 1;
            GetFavouriteGenreDetailsQueryValidator validator = new GetFavouriteGenreDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().Be(0);
        }
    }
}