using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.OrderOperations.Commands.CreateCommand
{
    public class CreateOrderCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateOrderCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongCustomerIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            CreateOrderViewModel model = new CreateOrderViewModel() { CustomerId = 565, FilmId = 1};

            //act
            CreateOrderCommand command = new CreateOrderCommand(_mapper,_context);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Customer could not be found!");
        }

        [Fact]
        public void WhenWrongFilmIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            CreateOrderViewModel model = new CreateOrderViewModel() { CustomerId = 1, FilmId = 465};

            //act
            CreateOrderCommand command = new CreateOrderCommand(_mapper,_context);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film could not be found!");
        }

        [Fact]
        public void WhenAlreadyExistFilmAndCustomerIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            var order = new Order() {CustomerId = 3 , FilmId = 4};
            _context.Orders.Add(order);
            _context.SaveChanges();

            CreateOrderViewModel model = new CreateOrderViewModel() { CustomerId = 3, FilmId = 4};

            //act
            CreateOrderCommand command = new CreateOrderCommand(_mapper,_context);
            command.Model = model;
            
            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Customer already bougth this film!");
        }

        [Fact]
        public void WhenNotExistFilmAndCustomerIdIsGiven_ActorFilmRelation_ShouldBeCreated()
        {
            // arrange
            CreateOrderViewModel model = new CreateOrderViewModel() { CustomerId = 4, FilmId = 4};
            CreateOrderCommand command = new CreateOrderCommand(_mapper,_context);
            command.Model = model;
        
            // act
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var order = _context.Orders.SingleOrDefault(s => s.CustomerId == model.CustomerId && s.FilmId == model.FilmId);
            
            order.Should().NotBeNull();
        }
    }
}