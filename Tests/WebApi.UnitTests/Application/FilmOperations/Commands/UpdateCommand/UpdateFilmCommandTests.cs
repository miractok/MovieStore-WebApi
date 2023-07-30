using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.FilmOperations.Commands.UpdateFilm;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.FilmOperations.Commands.UpdateCommand
{
    public class UpdateFilmCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateFilmCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange (Hazırlık)
            var film = new Film() {Title = "Test_WhenInvalidIdIsGiven_InvalidOperationException_ShouldReturn", PublishDate = new DateTime(1957,08,12), GenreId = 3, Price = 25};
            _context.Films.Add(film);
            _context.SaveChanges();

            UpdateFilmCommand command = new UpdateFilmCommand(_context);
            command.Model = new UpdateFilmModel() { Title = film.Title };

            //act & asssert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film could not be found!");
            
        }

        [Fact]
        public void WhenAlreadyExistFilmNameIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange (Hazırlık)
            var film = new Film() {Title = "Test_WhenAlreadyExistFilmNameIsGiven_InvalidOperationException_ShouldReturn1", PublishDate = new DateTime(1957,08,12), GenreId = 3, Price = 25};
            _context.Films.Add(film);
            _context.SaveChanges();

            UpdateFilmCommand command = new UpdateFilmCommand(_context);
            command.Model = new UpdateFilmModel() { Title = film.Title };
            command.FilmId = 1;

            //act & asssert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film already exists!");
            
        }
        
        //Happy Case
        [Fact]
        public void WhenValidInputsAreGiven_Film_ShouldBeUpdated()
        {
            //arrange
            UpdateFilmCommand command = new UpdateFilmCommand(_context);
            UpdateFilmModel model = new UpdateFilmModel() {Title="TestFilmName",PublishDate = DateTime.Now.Date.AddYears(-69) , GenreId = 2, Price = 65};
            command.Model = model;
            command.FilmId = 1;

            //act

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var updatefilm = _context.Films.SingleOrDefault(film => film.Title == model.Title);
            updatefilm.Should().NotBeNull();
            updatefilm.PublishDate.Should().Be(model.PublishDate);
            updatefilm.GenreId.Should().Be(model.GenreId);
            updatefilm.Price.Should().Be(model.Price);
        }
    }
}