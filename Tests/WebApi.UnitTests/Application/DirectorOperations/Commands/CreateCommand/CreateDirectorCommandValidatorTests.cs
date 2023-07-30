using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;

namespace Applications.DirectorOperations.Commands.CreateCommand
{
    public class CreateDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData("L")]
        [InlineData(" ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string namesurname)
        {
            //arrange
            CreateDirectorCommand command = new CreateDirectorCommand(null, null);
            command.Model = new CreateDirectorViewModel()
            {
                NameSurname = ""
            };

            //act
            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
        {
            //arrange
            CreateDirectorCommand command = new CreateDirectorCommand(null, null);
            command.Model = new CreateDirectorViewModel()
            {
                NameSurname = "DirectorNameTest"
            };

            //act
            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}