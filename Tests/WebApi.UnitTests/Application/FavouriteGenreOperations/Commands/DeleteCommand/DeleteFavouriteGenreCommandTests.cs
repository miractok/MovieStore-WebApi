using FluentAssertions;
using TestSetup;
using WebApi.Application.FavouriteGenreOperations.Commands.DeleteFavouriteGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.FavouriteGenreOperations.Commands.DeleteCommand
{
    public class DeleteFavouriteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteFavouriteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenDataIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            DeleteFavouriteGenreCommand command = new DeleteFavouriteGenreCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Data could not be found!");
        }
        //Happy Case
        [Fact]
        public void WhenDataIdIsValid_Film_ShouldBeDeleted()
        {
            var favouriteGenre = new FavouriteGenre() {CustomerId = 2, GenreId = 4};
            _context.FavouriteGenres.Add(favouriteGenre);
            _context.SaveChanges();

            DeleteFavouriteGenreCommand command = new DeleteFavouriteGenreCommand(_context);    
            command.DataId = favouriteGenre.Id;


            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var dataCheck = _context.FavouriteGenres.SingleOrDefault(dataCheck=> dataCheck.Id == favouriteGenre.Id);
            dataCheck.Should().BeNull();
        }
    }
}