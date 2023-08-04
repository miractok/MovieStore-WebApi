using FluentAssertions;
using TestSetup;
using WebApi.Application.FavouriteGenreOperations.Commands.DeleteFavouriteGenre;

namespace Applications.FavouriteGenreOperations.Commands.DeleteCommand
{
    public class DeleteFavouriteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    { 
        [Fact]
        public void WhenDataIdIsInvalid_Validator_ShouldBeReturnError()
        {
            //arrange
            DeleteFavouriteGenreCommand command = new DeleteFavouriteGenreCommand(null);
            command.DataId = 0;

            //act
            DeleteFavouriteGenreCommandValidator validator = new DeleteFavouriteGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDataIdIsValid_Validator_ShouldNotBeReturnError()
        {
            DeleteFavouriteGenreCommand command = new DeleteFavouriteGenreCommand(null);
            command.DataId = 1;

            DeleteFavouriteGenreCommandValidator validator = new DeleteFavouriteGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}