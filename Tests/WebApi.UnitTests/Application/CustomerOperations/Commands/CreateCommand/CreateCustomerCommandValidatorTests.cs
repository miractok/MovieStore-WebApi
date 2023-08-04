using FluentAssertions;
using TestSetup;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;

namespace Applications.CustomerOperations.Commands.CreateCommand
{
    public class CreateCustomerCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("testNameSurname", "", "")]
        [InlineData("testNameSurname", "TestEmail", "")]
        [InlineData("testNameSurname", "", "123465798")]
        [InlineData("", "", "")]
        [InlineData("", "TestEmail", "")]
        [InlineData("", "", "123465798")]
        [InlineData("lll", "", "")]
        [InlineData("lll", "TestEmail", "")]
        [InlineData("lll", "", "123465798")]
        [InlineData(" ", "TestEmail", "123465798")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string namesurname, string email, string password)
        {
            //arrange
            CreateCustomerCommand command = new CreateCustomerCommand(null, null);
            command.Model = new CreateCustomerModel()
            {
                NameSurname = "",
                Email = "",
                Password = ""
            };

            //act
            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
        {
            //arrange
            CreateCustomerCommand command = new CreateCustomerCommand(null, null);
            command.Model = new CreateCustomerModel()
            {
                NameSurname = "TestNameSurname",
                Email = "TestEmail@gmail.com",
                Password = "123123456456"
            };

            //act
            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}