using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorFilmOperations.Queries.GetDirectorFilmDetails;

namespace Applications.DirectorFilmOperations.Queries.GetDirectorFilmDetails
{
    public class GetDirectorFilmDetailsQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            GetDirectorFilmDetailsQuery query = new GetDirectorFilmDetailsQuery(null, null);
            query.DirectorFilmId = 0;
            GetDirectorFilmDetailsQueryValidator validator = new GetDirectorFilmDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldGetDetail()
        {
            GetDirectorFilmDetailsQuery query = new GetDirectorFilmDetailsQuery(null, null);
            query.DirectorFilmId = 1;
            GetDirectorFilmDetailsQueryValidator validator = new GetDirectorFilmDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().Be(0);
        }
    }
}