using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.FilmOperations.Queries.GetFilmDetails;
using WebApi.DBOperations;

namespace Applications.FilmOperations.Queries.GetFilmDetails
{
    public class GetFilmDetailsQueryTests : IClassFixture<CommonTestFixture>
    {
        readonly MovieStoreDbContext _context;
        readonly IMapper _mapper;

        public GetFilmDetailsQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongFilmIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            GetFilmDetailsQuery query = new GetFilmDetailsQuery(_context, _mapper);
            query.FilmId = -1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("The Id you entered does not match any film.");
        }

        [Fact]
        public void WhenValidFilmIdIsGiven_Film_ShouldReturn()
        {
            GetFilmDetailsQuery query = new GetFilmDetailsQuery(_context, _mapper);
            query.FilmId = 1;
            FluentActions.Invoking(() => query.Handle()).Invoke();

            var film = _context.Films.SingleOrDefault(film => film.Id == 1);
            film.Should().NotBeNull();
        }
    }
}