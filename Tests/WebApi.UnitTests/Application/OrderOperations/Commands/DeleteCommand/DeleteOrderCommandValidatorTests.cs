using FluentAssertions;
using TestSetup;
using WebApi.Application.OrderOperations.Commands.DeleteOrder;

namespace Applications.OrderOperations.Commands.DeleteCommand
{
    public class DeleteOrderCommandValidatorTests : IClassFixture<CommonTestFixture>
    { 
        [Fact]
        public void WhenDataIdIsInvalid_Validator_ShouldBeReturnError()
        {
            //arrange
            DeleteOrderCommand command = new DeleteOrderCommand(null);
            command.DataId = 0;

            //act
            DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDataIdIsValid_Validator_ShouldNotBeReturnError()
        {
            DeleteOrderCommand command = new DeleteOrderCommand(null);
            command.DataId = 1;

            DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}