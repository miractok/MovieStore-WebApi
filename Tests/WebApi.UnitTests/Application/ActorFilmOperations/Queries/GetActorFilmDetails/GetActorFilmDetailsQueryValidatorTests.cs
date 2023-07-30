using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorFilmOperations.Queries.GetActorFilmDetails;

namespace Applications.ActorFilmOperations.Queries.GetActorFilmDetails
{
    public class GetActorFilmDetailsQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            GetActorFilmDetailsQuery query = new GetActorFilmDetailsQuery(null, null);
            query.ActorFilmId = 0;
            GetActorFilmDetailsQueryValidator validator = new GetActorFilmDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldGetDetail()
        {
            GetActorFilmDetailsQuery query = new GetActorFilmDetailsQuery(null, null);
            query.ActorFilmId = 1;
            GetActorFilmDetailsQueryValidator validator = new GetActorFilmDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().Be(0);
        }
    }
}