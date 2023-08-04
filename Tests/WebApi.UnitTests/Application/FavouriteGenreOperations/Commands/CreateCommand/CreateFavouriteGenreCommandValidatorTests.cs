using FluentAssertions;
using TestSetup;
using WebApi.Application.FavouriteGenreOperations.Commands.CreateFavouriteGenre;

namespace Applications.FavouriteGenreOperations.Commands.CreateCommand
{
    public class CreateFavouriteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,0)]
        [InlineData(1,0)]
        [InlineData(0,1)]
        [InlineData(null,1)]
        [InlineData(1,null)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int customerid, int genreid)
        {
            //arrange
            CreateFavouriteGenreCommand command = new CreateFavouriteGenreCommand(null, null);
            command.Model = new CreateFavouriteGenreViewModel()
            {
                CustomerId = 0,
                GenreId = 0
            };

            //act
            CreateFavouriteGenreCommandValidator validator = new CreateFavouriteGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
        {
            //arrange
            CreateFavouriteGenreCommand command = new CreateFavouriteGenreCommand(null, null);
            command.Model = new CreateFavouriteGenreViewModel()
            {
                CustomerId = 1,
                GenreId = 1
            };

            //act
            CreateFavouriteGenreCommandValidator validator = new CreateFavouriteGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}