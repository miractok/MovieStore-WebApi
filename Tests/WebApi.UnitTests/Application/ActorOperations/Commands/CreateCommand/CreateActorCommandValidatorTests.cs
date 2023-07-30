using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorOperations.Commands.CreateActor;

namespace Applications.ActorOperations.Commands.CreateCommand
{
    public class CreateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData("L")]
        [InlineData(" ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string namesurname)
        {
            //arrange
            CreateActorCommand command = new CreateActorCommand(null, null);
            command.Model = new CreateActorViewModel()
            {
                NameSurname = ""
            };

            //act
            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
        {
            //arrange
            CreateActorCommand command = new CreateActorCommand(null, null);
            command.Model = new CreateActorViewModel()
            {
                NameSurname = "ActorNameTest"
            };

            //act
            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}