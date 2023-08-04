using FluentAssertions;
using TestSetup;
using WebApi.Application.OrderOperations.Commands.UpdateOrder;

namespace Applications.OrderOperations.Commands.UpdateCommand
{
    public class UpdateOrderCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData(0,0)]
        [InlineData(1,0)]
        [InlineData(0,1)]
        [InlineData(null,1)]
        [InlineData(1,null)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int customerid, int filmid)
        {
            //arrange
            UpdateOrderCommand command = new UpdateOrderCommand(null);
            command.Model = new UpdateOrderModel()
            {
                CustomerId = customerid,
                FilmId = filmid
            };

            //act
           UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            var dataid =1;
            UpdateOrderCommand command = new UpdateOrderCommand(null);
            command.DataId = dataid;
            command.Model = new UpdateOrderModel()
            {
                CustomerId = 4,
                FilmId = 4
            };

            UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}