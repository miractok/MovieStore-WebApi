using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorFilmOperations.Commands.CreateActorFilm;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.ActorFilmOperations.Commands.CreateCommand
{
    public class CreateActorFilmCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateActorFilmCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongActorIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            CreateActorFilmViewModel model = new CreateActorFilmViewModel() { ActorId = 565, FilmId = 1};

            //act
            CreateActorFilmCommand command = new CreateActorFilmCommand(_mapper,_context);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Actor could not be found!");
        }

        [Fact]
        public void WhenWrongFilmIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            CreateActorFilmViewModel model = new CreateActorFilmViewModel() { ActorId = 1, FilmId = 465};

            //act
            CreateActorFilmCommand command = new CreateActorFilmCommand(_mapper,_context);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film could not be found!");
        }

        [Fact]
        public void WhenAlreadyExistMovieAndActorIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            var actorFilm = new ActorFilm() {ActorId = 3 , FilmId = 4};
            _context.ActorFilms.Add(actorFilm);
            _context.SaveChanges();

            CreateActorFilmViewModel model = new CreateActorFilmViewModel() { ActorId = 3, FilmId = 4};

            //act
            CreateActorFilmCommand command = new CreateActorFilmCommand(_mapper,_context);
            command.Model = model;
            
            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("This relation already exists.");
        }

        [Fact]
        public void WhenNotExistMovieAndActorIdIsGiven_ActorFilmRelation_ShouldBeCreated()
        {
            // arrange
            CreateActorFilmViewModel model = new CreateActorFilmViewModel() { ActorId = 4, FilmId = 4};
            CreateActorFilmCommand command = new CreateActorFilmCommand(_mapper,_context);
            command.Model = model;
        
            // act
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var actorFilms = _context.ActorFilms.SingleOrDefault(s => s.ActorId == model.ActorId && s.FilmId == model.FilmId);
            
            actorFilms.Should().NotBeNull();
        }
    }
}