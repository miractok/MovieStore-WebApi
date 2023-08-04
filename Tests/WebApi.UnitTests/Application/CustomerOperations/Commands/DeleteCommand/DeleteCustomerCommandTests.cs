using FluentAssertions;
using TestSetup;
using WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.CustomerOperations.Commands.DeleteCommand
{
    public class DeleteCustomerCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteCustomerCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenCustomerIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Customer could not be found!");
        }
        //Happy Case
        [Fact]
        public void WhenCustoemrIdIsValid_Film_ShouldBeDeleted()
        {
            var customer = new Customer() {NameSurname = "Test_WhenCustomerIdIsValid_Film_ShouldBeDeleted", Email = "TestCustomer@gmail.com", Password = "12345678543"};
            _context.Customers.Add(customer);
            _context.SaveChanges();

            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);    
            command.CustomerId = customer.Id;


            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var customerCheck = _context.Customers.SingleOrDefault(customerCheck=> customerCheck.Id == customer.Id);
            customerCheck.Should().BeNull();
        }
    }
}