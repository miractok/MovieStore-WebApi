using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorFilmOperations.Queries.GetDirectorFilmDetails;
using WebApi.DBOperations;

namespace Applications.DirectorFilmOperations.Queries.GetDirectorFilmDetails
{
    public class GetDirectorFilmDetailsQueryTests : IClassFixture<CommonTestFixture>
    {
        readonly MovieStoreDbContext _context;
        readonly IMapper _mapper;

        public GetDirectorFilmDetailsQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongActorIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            GetDirectorFilmDetailsQuery query = new GetDirectorFilmDetailsQuery(_context, _mapper);
            query.DirectorFilmId = -1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("The Id you entered does not match any DirectorFilm relation.");
        }

        [Fact]
        public void WhenValidDirectorFilmIdIsGiven_DirectorFilm_ShouldReturn()
        {
            GetDirectorFilmDetailsQuery query = new GetDirectorFilmDetailsQuery(_context, _mapper);
            query.DirectorFilmId = 1;
            FluentActions.Invoking(() => query.Handle()).Invoke();

            var directorFilm = _context.DirectorFilms.SingleOrDefault(directorFilm => directorFilm.Id == 1);
            directorFilm.Should().NotBeNull();
        }
    }
}