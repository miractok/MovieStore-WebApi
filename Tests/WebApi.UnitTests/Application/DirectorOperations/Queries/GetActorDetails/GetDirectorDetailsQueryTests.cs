using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetails;
using WebApi.DBOperations;

namespace Applications.DirectorOperations.Queries.GetDirectorDetails
{
    public class GetDirectorDetailsQueryTests : IClassFixture<CommonTestFixture>
    {
        readonly MovieStoreDbContext _context;
        readonly IMapper _mapper;

        public GetDirectorDetailsQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongDirectorIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            GetDirectorDetailsQuery query = new GetDirectorDetailsQuery(_mapper, _context);
            query.DirectorId = -1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("The Id you entered does not match any director.");
        }

        [Fact]
        public void WhenValidDirectorIdIsGiven_Director_ShouldReturn()
        {
            GetDirectorDetailsQuery query = new GetDirectorDetailsQuery(_mapper, _context);
            query.DirectorId = 1;
            FluentActions.Invoking(() => query.Handle()).Invoke();

            var director = _context.Directors.SingleOrDefault(director => director.Id == 1);
            director.Should().NotBeNull();
        }
    }
}