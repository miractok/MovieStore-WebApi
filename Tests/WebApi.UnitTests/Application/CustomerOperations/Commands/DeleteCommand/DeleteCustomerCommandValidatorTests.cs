using FluentAssertions;
using TestSetup;
using WebApi.Application.CustomerOperations.Commands.DeleteCustomer;

namespace Applications.CustomerOperations.Commands.DeleteCommand
{
    public class DeleteCustomerCommandValidatorTests : IClassFixture<CommonTestFixture>
    { 
        [Fact]
        public void WhenCustomerIdIsInvalid_Validator_ShouldBeReturnError()
        {
            //arrange
            DeleteCustomerCommand command = new DeleteCustomerCommand(null);
            command.CustomerId = 0;

            //act
            DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenCustomerIdIsValid_Validator_ShouldNotBeReturnError()
        {
            DeleteCustomerCommand command = new DeleteCustomerCommand(null);
            command.CustomerId = 1;

            DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}