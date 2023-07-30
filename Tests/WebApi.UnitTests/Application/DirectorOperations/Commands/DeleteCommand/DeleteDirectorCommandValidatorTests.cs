using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorOperations.Commands.DeleteDirector;

namespace Applications.DirectorOperations.Commands.DeleteCommand
{
    public class DeleteDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
    { 
        [Fact]
        public void WhenDirectorIdIsInvalid_Validator_ShouldBeReturnError()
        {
            //arrange
            DeleteDirectorCommand command = new DeleteDirectorCommand(null);
            command.DirectorId = 0;

            //act
            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDirectorIdIsValid_Validator_ShouldNotBeReturnError()
        {
            DeleteDirectorCommand command = new DeleteDirectorCommand(null);
            command.DirectorId = 1;

            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}