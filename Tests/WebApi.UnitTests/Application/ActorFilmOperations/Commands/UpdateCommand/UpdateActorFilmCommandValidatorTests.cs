using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorFilmOperations.Commands.UpdateActorFilm;

namespace Applications.ActorFilmOperations.Commands.UpdateCommand
{
    public class UpdateActorFilmCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData(0,0)]
        [InlineData(1,0)]
        [InlineData(0,1)]
        [InlineData(null,1)]
        [InlineData(1,null)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int actorid, int filmid)
        {
            //arrange
            UpdateActorFilmCommand command = new UpdateActorFilmCommand(null);
            command.Model = new UpdateActorFilmModel()
            {
                ActorId = actorid,
                FilmId = filmid
            };

            //act
           UpdateActorFilmCommandValidator validator = new UpdateActorFilmCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            var dataid =1;
            UpdateActorFilmCommand command = new UpdateActorFilmCommand(null);
            command.DataId = dataid;
            command.Model = new UpdateActorFilmModel()
            {
                ActorId = 4,
                FilmId = 4
            };

            UpdateActorFilmCommandValidator validator = new UpdateActorFilmCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}