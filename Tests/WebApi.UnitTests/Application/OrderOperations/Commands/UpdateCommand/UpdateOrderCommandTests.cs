using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.OrderOperations.Commands.UpdateOrder;
using WebApi.DBOperations;

namespace Applications.OrderOperations.Commands.UpdateCommand
{
    public class UpdateOrderCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateOrderCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenDataIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            UpdateOrderCommand command = new UpdateOrderCommand(_context);
            command.DataId = 789;

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Order could not be found!");
        }

        [Fact]
        public void WhenWrongCustomerIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            UpdateOrderModel model = new UpdateOrderModel() { CustomerId = 782, FilmId = 1};

            //act
            UpdateOrderCommand command = new UpdateOrderCommand(_context);
            command.Model = model;
            command.DataId = 2;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Customer could not be found!");
        }

        [Fact]
        public void WhenWrongFilmIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            UpdateOrderModel model = new UpdateOrderModel() { CustomerId = 1, FilmId = 652};

            //act
            UpdateOrderCommand command = new UpdateOrderCommand(_context);
            command.Model = model;
            command.DataId = 3;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film could not be found!");
        }

        
        //Happy Case
        [Fact]
        public void WhenValidInputsAreGiven_Order_ShouldBeUpdated()
        {
            //arrange
            UpdateOrderCommand command = new UpdateOrderCommand(_context);
            UpdateOrderModel model = new UpdateOrderModel() {CustomerId = 1, FilmId = 3};
            command.Model = model;
            command.DataId = 3;

            //act

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var updateOrder = _context.Orders.SingleOrDefault(actorfilm => actorfilm.CustomerId == model.CustomerId && actorfilm.FilmId == model.FilmId);
            updateOrder.Should().NotBeNull();
        }
    }
}