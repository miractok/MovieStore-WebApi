using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorFilmOperations.Commands.UpdateDirectorFilm;

namespace Applications.DirectorFilmOperations.Commands.UpdateCommand
{
    public class UpdateDirectorFilmCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData(0,0)]
        [InlineData(1,0)]
        [InlineData(0,1)]
        [InlineData(null,1)]
        [InlineData(1,null)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int directorid, int filmid)
        {
            //arrange
            UpdateDirectorFilmCommand command = new UpdateDirectorFilmCommand(null);
            command.Model = new UpdateDirectorFilmModel()
            {
                DirectorId = directorid,
                FilmId = filmid
            };

            //act
           UpdateDirectorFilmCommandValidator validator = new UpdateDirectorFilmCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            var dataid =1;
            UpdateDirectorFilmCommand command = new UpdateDirectorFilmCommand(null);
            command.DataId = dataid;
            command.Model = new UpdateDirectorFilmModel()
            {
                DirectorId = 4,
                FilmId = 4
            };

            UpdateDirectorFilmCommandValidator validator = new UpdateDirectorFilmCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}