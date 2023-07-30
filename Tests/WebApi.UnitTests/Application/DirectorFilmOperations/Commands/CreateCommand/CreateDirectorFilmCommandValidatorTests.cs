using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorFilmOperations.Commands.CreateDirectorFilm;

namespace Applications.DirectorFilmOperations.Commands.CreateCommand
{
    public class CreateDirectorFilmCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,0)]
        [InlineData(1,0)]
        [InlineData(0,1)]
        [InlineData(null,1)]
        [InlineData(1,null)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int directorid, int filmid)
        {
            //arrange
            CreateDirectorFilmCommand command = new CreateDirectorFilmCommand(null, null);
            command.Model = new CreateDirectorFilmViewModel()
            {
                DirectorId = 0,
                FilmId = 0
            };

            //act
            CreateDirectorFilmCommandValidator validator = new CreateDirectorFilmCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
        {
            //arrange
            CreateDirectorFilmCommand command = new CreateDirectorFilmCommand(null, null);
            command.Model = new CreateDirectorFilmViewModel()
            {
                DirectorId = 1,
                FilmId = 1
            };

            //act
            CreateDirectorFilmCommandValidator validator = new CreateDirectorFilmCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}