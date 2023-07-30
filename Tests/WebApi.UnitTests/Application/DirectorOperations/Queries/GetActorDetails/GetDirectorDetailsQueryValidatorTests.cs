using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorOperations.Queries.GetActorDetails;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetails;

namespace Applications.DirectorOperations.Queries.GetDirectorDetails
{
    public class GetDirectorDetailsQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            GetDirectorDetailsQuery query = new GetDirectorDetailsQuery(null, null);
            query.DirectorId = 0;
            GetDirectorDetailsQueryValidator validator = new GetDirectorDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldGetDetail()
        {
            GetDirectorDetailsQuery query = new GetDirectorDetailsQuery(null, null);
            query.DirectorId = 1;
            GetDirectorDetailsQueryValidator validator = new GetDirectorDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().Be(0);
        }
    }
}