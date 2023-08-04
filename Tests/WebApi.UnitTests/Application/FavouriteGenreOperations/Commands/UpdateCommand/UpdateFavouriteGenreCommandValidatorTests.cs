using FluentAssertions;
using TestSetup;
using WebApi.Application.FavouriteGenreOperations.Commands.UpdateFavouriteGenre;

namespace Applications.FavouriteGenreOperations.Commands.UpdateCommand
{
    public class UpdateFavouriteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData(0,0)]
        [InlineData(1,0)]
        [InlineData(0,1)]
        [InlineData(null,1)]
        [InlineData(1,null)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int customerid, int genreid)
        {
            //arrange
            UpdateFavouriteGenreCommand command = new UpdateFavouriteGenreCommand(null);
            command.Model = new UpdateFavouriteGenreModel()
            {
                CustomerId = customerid,
                GenreId = genreid
            };

            //act
           UpdateFavouriteGenreCommandValidator validator = new UpdateFavouriteGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            var dataid =1;
            UpdateFavouriteGenreCommand command = new UpdateFavouriteGenreCommand(null);
            command.DataId = dataid;
            command.Model = new UpdateFavouriteGenreModel()
            {
                CustomerId = 4,
                GenreId = 4
            };

            UpdateFavouriteGenreCommandValidator validator = new UpdateFavouriteGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}