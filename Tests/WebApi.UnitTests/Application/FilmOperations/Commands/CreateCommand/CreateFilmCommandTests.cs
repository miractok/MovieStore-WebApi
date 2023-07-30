using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.FilmOperations.Commands.CreateFilm;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.FilmOperations.Commands.CreateCommand
{
    public class CreateFilmCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateFilmCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistFilmTitleIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            var film = new Film() {Title = "Test_WhenAlreadyExistFilmTitleIsGiven_InvalidOperationException_ShouldBeReturn", PublishDate = new DateTime(2001,08,12), GenreId = 1, Price = 10};
            _context.Films.Add(film);
            _context.SaveChanges();

            CreateFilmCommand command = new CreateFilmCommand(_mapper,_context);
            command.Model = new CreateFilmModel(){ Title = film.Title};
            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film already exists!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Film_ShouldCreated()
        {
            //arrange
            CreateFilmCommand command = new CreateFilmCommand(_mapper,_context);
            CreateFilmModel model = new CreateFilmModel(){ Title = "TestFilm", PublishDate = DateTime.Now.Date.AddYears(-32), GenreId = 2, Price = 134};
            command.Model = model;
            
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var film = _context.Films.SingleOrDefault(film => film.Title == model.Title);

            film.Should().NotBeNull();
            film.PublishDate.Should().Be(model.PublishDate);
            film.GenreId.Should().Be(model.GenreId); 
            film.Price.Should().Be(model.Price);
        }

    }
}