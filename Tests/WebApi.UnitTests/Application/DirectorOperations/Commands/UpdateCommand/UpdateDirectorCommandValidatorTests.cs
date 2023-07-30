using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;

namespace Applications.DirectorOperations.Commands.UpdateCommand
{
    public class UpdateDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData(" ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string namesurname)
        {
            //arrange
            UpdateDirectorCommand command = new UpdateDirectorCommand(null);
            command.Model = new UpdateDirectorViewModel()
            {
                NameSurname = namesurname
            };

            //act
           UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            var directorid =1;
            UpdateDirectorCommand command = new UpdateDirectorCommand(null);
            command.DirectorId = directorid;
            command.Model = new UpdateDirectorViewModel()
            {
                NameSurname = "TestDirectorName"
            };

            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}