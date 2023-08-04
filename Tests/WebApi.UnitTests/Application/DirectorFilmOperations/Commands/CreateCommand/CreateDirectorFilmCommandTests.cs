using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorFilmOperations.Commands.CreateDirectorFilm;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.DirectorFilmOperations.Commands.CreateCommand
{
    public class CreateDirectorFilmCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateDirectorFilmCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongDirectorIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            CreateDirectorFilmViewModel model = new CreateDirectorFilmViewModel() { DirectorId = 565, FilmId = 1};

            //act
            CreateDirectorFilmCommand command = new CreateDirectorFilmCommand(_mapper,_context);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Director could not be found!");
        }

        [Fact]
        public void WhenWrongFilmIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            CreateDirectorFilmViewModel model = new CreateDirectorFilmViewModel() { DirectorId = 1, FilmId = 465};

            //act
            CreateDirectorFilmCommand command = new CreateDirectorFilmCommand(_mapper,_context);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film could not be found!");
        }

        [Fact]
        public void WhenAlreadyExistMovieAndDirectorIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            var directorFilm = new DirectorFilm() {DirectorId = 4 , FilmId = 3};
            _context.DirectorFilms.Add(directorFilm);
            _context.SaveChanges();

            CreateDirectorFilmViewModel model = new CreateDirectorFilmViewModel() { DirectorId = 4, FilmId = 3};

            //act
            CreateDirectorFilmCommand command = new CreateDirectorFilmCommand(_mapper,_context);
            command.Model = model;
            
            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("This relation already exists.");
        }

        [Fact]
        public void WhenNotExistFilmAndDirectorIdIsGiven_DirectorFilmRelation_ShouldBeCreated()
        {
            // arrange
            CreateDirectorFilmViewModel model = new CreateDirectorFilmViewModel() { DirectorId = 4, FilmId = 4};
            CreateDirectorFilmCommand command = new CreateDirectorFilmCommand(_mapper,_context);
            command.Model = model;
        
            // act
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var directorFilms = _context.DirectorFilms.SingleOrDefault(s => s.DirectorId == model.DirectorId && s.FilmId == model.FilmId);
            
            directorFilms.Should().NotBeNull();
        }
    }
}