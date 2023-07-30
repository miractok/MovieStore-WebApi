using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorFilmOperations.Commands.DeleteActorFilm;

namespace Applications.ActorFilmOperations.Commands.DeleteCommand
{
    public class DeleteActorFilmCommandValidatorTests : IClassFixture<CommonTestFixture>
    { 
        [Fact]
        public void WhenDataIdIsInvalid_Validator_ShouldBeReturnError()
        {
            //arrange
            DeleteActorFilmCommand command = new DeleteActorFilmCommand(null);
            command.DataId = 0;

            //act
            DeleteActorFilmCommandValidator validator = new DeleteActorFilmCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDataIdIsValid_Validator_ShouldNotBeReturnError()
        {
            DeleteActorFilmCommand command = new DeleteActorFilmCommand(null);
            command.DataId = 1;

            DeleteActorFilmCommandValidator validator = new DeleteActorFilmCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}