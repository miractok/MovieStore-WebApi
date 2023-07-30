using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorOperations.Queries.GetActorDetails;

namespace Applications.ActorOperations.Queries.GetActorDetails
{
    public class GetActorDetailsQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            GetActorDetailsQuery query = new GetActorDetailsQuery(null, null);
            query.ActorId = 0;
            GetActorDetailsQueryValidator validator = new GetActorDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldGetDetail()
        {
            GetActorDetailsQuery query = new GetActorDetailsQuery(null, null);
            query.ActorId = 1;
            GetActorDetailsQueryValidator validator = new GetActorDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().Be(0);
        }
    }
}