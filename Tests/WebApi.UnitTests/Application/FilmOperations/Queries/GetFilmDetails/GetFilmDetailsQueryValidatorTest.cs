using FluentAssertions;
using TestSetup;
using WebApi.Application.FilmOperations.Queries.GetFilmDetails;

namespace Applications.FilmOperations.Queries.GetFilmDetails
{
    public class GetFilmDetailsQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            GetFilmDetailsQuery query = new GetFilmDetailsQuery(null, null);
            query.FilmId = 0;
            GetFilmDetailsQueryValidator validator = new GetFilmDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldGetDetail()
        {
            GetFilmDetailsQuery query = new GetFilmDetailsQuery(null, null);
            query.FilmId = 1;
            GetFilmDetailsQueryValidator validator = new GetFilmDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().Be(0);
        }
    }
}