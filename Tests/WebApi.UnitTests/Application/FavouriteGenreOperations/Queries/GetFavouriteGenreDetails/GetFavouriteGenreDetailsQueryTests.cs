using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.FavouriteGenreOperations.Queries.GetFavouriteGenreDetails;
using WebApi.DBOperations;

namespace Applications.FavouriteGenreOperations.Queries.GetFavouriteGenreDetails
{
    public class GetFavouriteGenreDetailsQueryTests : IClassFixture<CommonTestFixture>
    {
        readonly MovieStoreDbContext _context;
        readonly IMapper _mapper;

        public GetFavouriteGenreDetailsQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongFavouriteGenreIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            GetFavouriteGenreDetailsQuery query = new GetFavouriteGenreDetailsQuery(_context, _mapper);
            query.favouriteGenreId = -1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("The Id you entered does not match any FavouriteGenre relation.");
        }

        [Fact]
        public void WhenValidFavouriteGenreIdIsGiven_FavouriteGenre_ShouldReturn()
        {
            GetFavouriteGenreDetailsQuery query = new GetFavouriteGenreDetailsQuery(_context, _mapper);
            query.favouriteGenreId = 1;
            FluentActions.Invoking(() => query.Handle()).Invoke();

            var favouriteGenre = _context.FavouriteGenres.SingleOrDefault(favouriteGenre => favouriteGenre.Id == 1);
            favouriteGenre.Should().NotBeNull();
        }
    }
}