using FluentAssertions;
using TestSetup;
using WebApi.Application.FilmOperations.Commands.CreateFilm;

namespace Applications.FilmOperations.Commands.CreateCommand
{
    public class CreateFilmCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord Of The Rings", 0, 0)]
        [InlineData("Lord Of The Rings", 1, 0)]
        [InlineData("Lord Of The Rings", 0, 1)]
        [InlineData("", 0, 0)]
        [InlineData("", 1, 0)]
        [InlineData("", 0, 1)]
        [InlineData("L", 0, 0)]
        [InlineData("L", 1, 0)]
        [InlineData("L", 0, 1)]
        [InlineData(" ", 1, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string title, int genreId, int price)
        {
            //arrange
            CreateFilmCommand command = new CreateFilmCommand(null, null);
            command.Model = new CreateFilmModel()
            {
                Title = "",
                PublishDate = DateTime.Now.Date,
                GenreId = 0,
                Price = 0
            };

            //act
            CreateFilmCommandValidator validator = new CreateFilmCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldReturnError()
        {
            CreateFilmCommand command = new CreateFilmCommand(null, null);
            command.Model = new CreateFilmModel()
            {
                Title = "Lord of the rings",
                PublishDate = DateTime.Now.Date,
                GenreId = 1,
                Price = 102
            };  

            CreateFilmCommandValidator validator = new CreateFilmCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
        {
            //arrange
            CreateFilmCommand command = new CreateFilmCommand(null, null);
            command.Model = new CreateFilmModel()
            {
                Title = "Lord of the rings",
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1,
                Price = 102
            };

            //act
            CreateFilmCommandValidator validator = new CreateFilmCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}