using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorFilmOperations.Commands.UpdateActorFilm;
using WebApi.DBOperations;

namespace Applications.ActorFilmOperations.Commands.UpdateCommand
{
    public class UpdateActorFilmCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateActorFilmCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenDataIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            UpdateActorFilmCommand command = new UpdateActorFilmCommand(_context);
            command.DataId = 789;

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Data could not be found!");
        }

        [Fact]
        public void WhenWrongActorIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            UpdateActorFilmModel model = new UpdateActorFilmModel() { ActorId = 782, FilmId = 1};

            //act
            UpdateActorFilmCommand command = new UpdateActorFilmCommand(_context);
            command.Model = model;
            command.DataId = 2;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Actor could not be found!");
        }

        [Fact]
        public void WhenWrongFilmIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            UpdateActorFilmModel model = new UpdateActorFilmModel() { ActorId = 1, FilmId = 487};

            //act
            UpdateActorFilmCommand command = new UpdateActorFilmCommand(_context);
            command.Model = model;
            command.DataId = 3;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film could not be found!");
        }

        
        //Happy Case
        [Fact]
        public void WhenValidInputsAreGiven_ActorFilmRelation_ShouldBeUpdated()
        {
            //arrange
            UpdateActorFilmCommand command = new UpdateActorFilmCommand(_context);
            UpdateActorFilmModel model = new UpdateActorFilmModel() {ActorId = 1, FilmId = 3};
            command.Model = model;
            command.DataId = 3;

            //act

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var updateActorFilm = _context.ActorFilms.SingleOrDefault(actorfilm => actorfilm.ActorId == model.ActorId && actorfilm.FilmId == model.FilmId);
            updateActorFilm.Should().NotBeNull();
        }
    }
}