using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorOperations.Commands.UpdateActor;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.ActorOperations.Commands.UpdateCommand
{
    public class UpdateActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenActorIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            UpdateActorCommand command = new UpdateActorCommand(_context);
            command.ActorId = 426;

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Actor could not be found!");
        }

        [Fact]
        public void WhenAlreadyExistActorNameIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange (Hazırlık)
            var actor = new Actor() {NameSurname = "Test_WhenAlreadyExistActorNameIsGiven_InvalidOperationException_ShouldReturn1"};
            _context.Actors.Add(actor);
            _context.SaveChanges();

            UpdateActorCommand command = new UpdateActorCommand(_context);
            command.Model = new UpdateActorModel() {  NameSurname = actor.NameSurname };
            command.ActorId = 1;

            //act & asssert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Actor already exists!");
            
        }
        
        //Happy Case
        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeUpdated()
        {
            //arrange
            UpdateActorCommand command = new UpdateActorCommand(_context);
            UpdateActorModel model = new UpdateActorModel() {NameSurname="TestActorName"};
            command.Model = model;
            command.ActorId = 1;

            //act

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var updateactor = _context.Actors.SingleOrDefault(actor => actor.NameSurname == model.NameSurname);
            updateactor.Should().NotBeNull();
        }
    }
}