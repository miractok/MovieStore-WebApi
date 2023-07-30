using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.ActorOperations.Commands.CreateCommand
{
    public class CreateActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistActorNameIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            var actor = new Actor() {NameSurname = "Test_WhenAlreadyExistActorNameIsGiven_InvalidOperationException_ShouldReturn"};
            _context.Actors.Add(actor);
            _context.SaveChanges();

            CreateActorCommand command = new CreateActorCommand(_mapper,_context);
            command.Model = new CreateActorViewModel(){ NameSurname = actor.NameSurname};
            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("This Actor already exists.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldCreated()
        {
            //arrange
            CreateActorCommand command = new CreateActorCommand(_mapper,_context);
            CreateActorViewModel model = new CreateActorViewModel(){ NameSurname = "TestActor"};
            command.Model = model;
            
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var actor = _context.Actors.SingleOrDefault(actor => actor.NameSurname == model.NameSurname);

            actor.Should().NotBeNull();
        }

    }
}