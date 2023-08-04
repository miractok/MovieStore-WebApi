using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.CustomerOperations.Commands.CreateCommand
{
    public class CreateCustomerCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateCustomerCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistCustomerEmailIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            var customer = new Customer() {NameSurname = "Test_WhenAlreadyExistCustomerEmailIsGiven_InvalidOperationException_ShouldReturn", Email = "TestCustomerr@gmail.com", Password = "1122334455"};
            _context.Customers.Add(customer);
            _context.SaveChanges();

            CreateCustomerCommand command = new CreateCustomerCommand(_mapper,_context);
            command.Model = new CreateCustomerModel(){ Email = customer.Email};
            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Customer already exists!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Customer_ShouldCreated()
        {
            //arrange
            CreateCustomerCommand command = new CreateCustomerCommand(_mapper,_context);
            CreateCustomerModel model = new CreateCustomerModel(){NameSurname = "Test_WhenValidInputsAreGiven_Customer_ShouldCreated", Email = "TestCustomer1@gmail.com", Password = "111222333444555"};
            command.Model = model;
            
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var customer = _context.Customers.SingleOrDefault(customer => customer.Email == model.Email);

            customer.Should().NotBeNull();
            customer.NameSurname.Should().Be(model.NameSurname);
            customer.Password.Should().Be(model.Password);
        }

    }
}