using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.OrderOperations.Queries.GetOrderDetails;
using WebApi.DBOperations;

namespace Applications.OrderOperations.Queries.GetOrderDetails
{
    public class GetOrderDetailsQueryTests : IClassFixture<CommonTestFixture>
    {
        readonly MovieStoreDbContext _context;
        readonly IMapper _mapper;

        public GetOrderDetailsQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongOrderIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            GetOrderDetailsQuery query = new GetOrderDetailsQuery(_context, _mapper);
            query.OrderId = -1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("The Id you entered does not match any Order relation.");
        }

        [Fact]
        public void WhenValidOrderIdIsGiven_Order_ShouldReturn()
        {
            GetOrderDetailsQuery query = new GetOrderDetailsQuery(_context, _mapper);
            query.OrderId = 1;
            FluentActions.Invoking(() => query.Handle()).Invoke();

            var order = _context.Orders.SingleOrDefault(order => order.Id == 1);
            order.Should().NotBeNull();
        }
    }
}