using FluentAssertions;
using TestSetup;
using WebApi.Application.FilmOperations.Commands.DeleteFilm;

namespace Applications.FilmOperations.Commands.DeleteCommand
{
    public class DeleteFilmCommandValidatorTests : IClassFixture<CommonTestFixture>
    { 
        [Fact]
        public void WhenFilmIdIsInvalid_Validator_ShouldBeReturnError()
        {
            //arrange
            DeleteFilmCommand command = new DeleteFilmCommand(null);
            command.FilmId = 0;

            //act
            DeleteFilmCommandValidator validator = new DeleteFilmCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenFilmIdIsValid_Validator_ShouldNotBeReturnError()
        {
            DeleteFilmCommand command = new DeleteFilmCommand(null);
            command.FilmId = 1;

            DeleteFilmCommandValidator validator = new DeleteFilmCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}