using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorOperations.Commands.DeleteActor;

namespace Applications.ActorOperations.Commands.DeleteCommand
{
    public class DeleteActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    { 
        [Fact]
        public void WhenActorIdIsInvalid_Validator_ShouldBeReturnError()
        {
            //arrange
            DeleteActorCommand command = new DeleteActorCommand(null);
            command.ActorId = 0;

            //act
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenActorIdIsValid_Validator_ShouldNotBeReturnError()
        {
            DeleteActorCommand command = new DeleteActorCommand(null);
            command.ActorId = 1;

            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}