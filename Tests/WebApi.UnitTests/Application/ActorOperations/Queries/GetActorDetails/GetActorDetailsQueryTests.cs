using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorOperations.Queries.GetActorDetails;
using WebApi.DBOperations;

namespace Applications.ActorOperations.Queries.GetActorDetails
{
    public class GetActorDetailsQueryTests : IClassFixture<CommonTestFixture>
    {
        readonly MovieStoreDbContext _context;
        readonly IMapper _mapper;

        public GetActorDetailsQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongActorIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            GetActorDetailsQuery query = new GetActorDetailsQuery(_context, _mapper);
            query.ActorId = -1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("The Id you entered does not match any actor.");
        }

        [Fact]
        public void WhenValidActorIdIsGiven_Actor_ShouldReturn()
        {
            GetActorDetailsQuery query = new GetActorDetailsQuery(_context, _mapper);
            query.ActorId = 1;
            FluentActions.Invoking(() => query.Handle()).Invoke();

            var actor = _context.Actors.SingleOrDefault(actor => actor.Id == 1);
            actor.Should().NotBeNull();
        }
    }
}