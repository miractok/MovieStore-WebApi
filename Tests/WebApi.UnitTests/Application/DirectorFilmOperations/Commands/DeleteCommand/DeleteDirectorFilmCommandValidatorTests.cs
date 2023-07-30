using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorFilmOperations.Commands.DeleteDirectorFilm;

namespace Applications.DirectorFilmOperations.Commands.DeleteCommand
{
    public class DeleteDirectorFilmCommandValidatorTests : IClassFixture<CommonTestFixture>
    { 
        [Fact]
        public void WhenDataIdIsInvalid_Validator_ShouldBeReturnError()
        {
            //arrange
            DeleteDirectorFilmCommand command = new DeleteDirectorFilmCommand(null);
            command.DataId = 0;

            //act
            DeleteDirectorFilmCommandValidator validator = new DeleteDirectorFilmCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDataIdIsValid_Validator_ShouldNotBeReturnError()
        {
            DeleteDirectorFilmCommand command = new DeleteDirectorFilmCommand(null);
            command.DataId = 1;

            DeleteDirectorFilmCommandValidator validator = new DeleteDirectorFilmCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}