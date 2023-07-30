using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorOperations.Commands.DeleteActor;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.ActorOperations.Commands.DeleteCommand
{
    public class DeleteActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenActorIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            DeleteActorCommand command = new DeleteActorCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Actor could not be found!");
        }
        //Happy Case
        [Fact]
        public void WhenActorIdIsValid_Film_ShouldBeDeleted()
        {
            var actor = new Actor() {NameSurname = "Test_WhenActorIdIsValid_Film_ShouldBeDeleted"};
            _context.Actors.Add(actor);
            _context.SaveChanges();

            DeleteActorCommand command = new DeleteActorCommand(_context);    
            command.ActorId = actor.Id;


            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var actorCheck = _context.Actors.SingleOrDefault(actorCheck=> actorCheck.Id == actor.Id);
            actorCheck.Should().BeNull();
        }
    }
}