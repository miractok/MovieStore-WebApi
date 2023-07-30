using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.DirectorOperations.Commands.DeleteCommand
{
    public class DeleteDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenDirectorIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Director could not be found!");
        }
        //Happy Case
        [Fact]
        public void WhenDirectorIdIsValid_Film_ShouldBeDeleted()
        {
            var director = new Director() {NameSurname = "Test_WhenDirectorIdIsValid_Film_ShouldBeDeleted"};
            _context.Directors.Add(director);
            _context.SaveChanges();

            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);    
            command.DirectorId = director.Id;


            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var directorCheck = _context.Directors.SingleOrDefault(directorCheck=> directorCheck.Id == director.Id);
            directorCheck.Should().BeNull();
        }
    }
}