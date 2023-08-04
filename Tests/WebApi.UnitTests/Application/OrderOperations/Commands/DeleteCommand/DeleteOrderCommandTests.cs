using FluentAssertions;
using TestSetup;
using WebApi.Application.OrderOperations.Commands.DeleteOrder;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.OrderOperations.Commands.DeleteCommand
{
    public class DeleteOrderCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteOrderCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenDataIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            DeleteOrderCommand command = new DeleteOrderCommand(_context);

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
            var order = new Order() {CustomerId = 2, FilmId = 4};
            _context.Orders.Add(order);
            _context.SaveChanges();

            DeleteOrderCommand command = new DeleteOrderCommand(_context);    
            command.DataId = order.Id;


            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var dataCheck = _context.Orders.SingleOrDefault(dataCheck=> dataCheck.Id == order.Id);
            dataCheck.Should().BeNull();
        }
    }
}