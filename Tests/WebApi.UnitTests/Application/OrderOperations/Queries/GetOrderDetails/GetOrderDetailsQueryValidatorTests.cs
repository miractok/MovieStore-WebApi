using FluentAssertions;
using TestSetup;
using WebApi.Application.OrderOperations.Queries.GetOrderDetails;

namespace Applications.OrderOperations.Queries.GetOrderDetails
{
    public class GetOrderDetailsQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            GetOrderDetailsQuery query = new GetOrderDetailsQuery(null, null);
            query.OrderId = 0;
            GetOrderDetailsQueryValidator validator = new GetOrderDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldGetDetail()
        {
            GetOrderDetailsQuery query = new GetOrderDetailsQuery(null, null);
            query.OrderId = 1;
            GetOrderDetailsQueryValidator validator = new GetOrderDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().Be(0);
        }
    }
}