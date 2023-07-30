using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorFilmOperations.Commands.CreateActorFilm;

namespace Applications.ActorFilmOperations.Commands.CreateCommand
{
    public class CreateActorFilmCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,0)]
        [InlineData(1,0)]
        [InlineData(0,1)]
        [InlineData(null,1)]
        [InlineData(1,null)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int actorid, int filmid)
        {
            //arrange
            CreateActorFilmCommand command = new CreateActorFilmCommand(null, null);
            command.Model = new CreateActorFilmViewModel()
            {
                ActorId = 0,
                FilmId = 0
            };

            //act
            CreateActorFilmCommandValidator validator = new CreateActorFilmCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
        {
            //arrange
            CreateActorFilmCommand command = new CreateActorFilmCommand(null, null);
            command.Model = new CreateActorFilmViewModel()
            {
                ActorId = 1,
                FilmId = 1
            };

            //act
            CreateActorFilmCommandValidator validator = new CreateActorFilmCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}