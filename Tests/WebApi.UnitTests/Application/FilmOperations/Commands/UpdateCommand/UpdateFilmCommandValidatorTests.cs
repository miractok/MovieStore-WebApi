using FluentAssertions;
using TestSetup;
using WebApi.Application.FilmOperations.Commands.UpdateFilm;

namespace Applications.FilmOperations.Commands.UpdateCommand
{
    public class UpdateFilmCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData("",0,0)]
        [InlineData("",1,0)]
        [InlineData("",0,1)]
        [InlineData("deneme",0,0)]
        [InlineData(" ",0,0)]
        [InlineData(" ",1,0)]
        [InlineData(" ",0,1)]
        [InlineData(" ",1,1)]
        [InlineData("d",0,0)]
        [InlineData("d",1,0)]
        [InlineData("d",0,1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title,int genreID, int price)
        {
            //arrange
            UpdateFilmCommand command = new UpdateFilmCommand(null);
            command.Model = new UpdateFilmModel()
            {
                Title = title,
                GenreId = genreID,
                Price = price
            };

            //act
            UpdateFilmCommandValidator validator = new UpdateFilmCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldReturnError()
        {
            UpdateFilmCommand command = new UpdateFilmCommand(null);
            command.Model = new UpdateFilmModel()
            {
                Title = "Test name bla bla",
                PublishDate = DateTime.Now.Date,
                GenreId = 3,
                Price = 147
            };  

            UpdateFilmCommandValidator validator = new UpdateFilmCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            var filmid =1;
            UpdateFilmCommand command = new UpdateFilmCommand(null);
            command.FilmId = filmid;
            command.Model = new UpdateFilmModel()
            {
                Title = "TestFilmName",
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 2,
                Price = 59
            };

            UpdateFilmCommandValidator validator = new UpdateFilmCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}