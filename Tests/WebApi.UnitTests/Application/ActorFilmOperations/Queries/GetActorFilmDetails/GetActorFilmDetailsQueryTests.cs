using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorFilmOperations.Queries.GetActorFilmDetails;
using WebApi.DBOperations;

namespace Applications.ActorFilmOperations.Queries.GetActorFilmDetails
{
    public class GetActorFilmDetailsQueryTests : IClassFixture<CommonTestFixture>
    {
        readonly MovieStoreDbContext _context;
        readonly IMapper _mapper;

        public GetActorFilmDetailsQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongActorIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            GetActorFilmDetailsQuery query = new GetActorFilmDetailsQuery(_context, _mapper);
            query.ActorFilmId = -1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("The Id you entered does not match any actorFilm relation.");
        }

        [Fact]
        public void WhenValidActorFilmIdIsGiven_ActorFilm_ShouldReturn()
        {
            GetActorFilmDetailsQuery query = new GetActorFilmDetailsQuery(_context, _mapper);
            query.ActorFilmId = 1;
            FluentActions.Invoking(() => query.Handle()).Invoke();

            var actorFilm = _context.ActorFilms.SingleOrDefault(actorFilm => actorFilm.Id == 1);
            actorFilm.Should().NotBeNull();
        }
    }
}