using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.CustomerOperations.Queries.GetCustomerDetails;
using WebApi.DBOperations;

namespace Applications.CustomerOperations.Queries.GetCustomerDetails
{
    public class GetCustomerDetailsQueryTests : IClassFixture<CommonTestFixture>
    {
        readonly MovieStoreDbContext _context;
        readonly IMapper _mapper;

        public GetCustomerDetailsQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongCustomerIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            GetCustomerDetailsQuery query = new GetCustomerDetailsQuery(_context, _mapper);
            query.CustomerId = -1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("The Id you entered does not match any customer.");
        }

        [Fact]
        public void WhenValidCustomerIdIsGiven_Customer_ShouldReturn()
        {
            GetCustomerDetailsQuery query = new GetCustomerDetailsQuery(_context, _mapper);
            query.CustomerId = 1;
            FluentActions.Invoking(() => query.Handle()).Invoke();

            var customer = _context.Customers.SingleOrDefault(customer => customer.Id == 1);
            customer.Should().NotBeNull();
        }
    }
}