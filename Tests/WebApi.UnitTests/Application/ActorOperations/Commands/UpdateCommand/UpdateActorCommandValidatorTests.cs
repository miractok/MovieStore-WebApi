using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorOperations.Commands.UpdateActor;

namespace Applications.ActorOperations.Commands.UpdateCommand
{
    public class UpdateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData(" ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string namesurname)
        {
            //arrange
            UpdateActorCommand command = new UpdateActorCommand(null);
            command.Model = new UpdateActorModel()
            {
                NameSurname = namesurname
            };

            //act
           UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            var actorid =1;
            UpdateActorCommand command = new UpdateActorCommand(null);
            command.ActorId = actorid;
            command.Model = new UpdateActorModel()
            {
                NameSurname = "TestActorName"
            };

            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}