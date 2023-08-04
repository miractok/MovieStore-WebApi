using FluentAssertions;
using TestSetup;
using WebApi.Application.OrderOperations.Commands.CreateOrder;

namespace Applications.OrderOperations.Commands.CreateCommand
{
    public class CreateOrderCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,0)]
        [InlineData(1,0)]
        [InlineData(0,1)]
        [InlineData(null,1)]
        [InlineData(1,null)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int customerid, int filmid)
        {
            //arrange
            CreateOrderCommand command = new CreateOrderCommand(null, null);
            command.Model = new CreateOrderViewModel()
            {
                CustomerId = 0,
                FilmId = 0
            };

            //act
            CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
        {
            //arrange
            CreateOrderCommand command = new CreateOrderCommand(null, null);
            command.Model = new CreateOrderViewModel()
            {
                CustomerId = 1,
                FilmId = 1
            };

            //act
            CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}