using FluentAssertions;
using TestSetup;
using WebApi.Application.FilmOperations.Commands.DeleteFilm;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.FilmOperations.Commands.DeleteCommand
{
    public class DeleteFilmCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteFilmCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenFilmIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            DeleteFilmCommand command = new DeleteFilmCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Film could not be found!");
        }
        //Happy Case
        [Fact]
        public void WhenFilmIdIsValid_Film_ShouldBeDeleted()
        {
            var film = new Film() {Title = "Test_WhenFilmIdIsValid_Film_ShouldBeDeleted", PublishDate = new DateTime(2001,08,12), GenreId = 1, Price = 10};
            _context.Films.Add(film);
            _context.SaveChanges();

            DeleteFilmCommand command = new DeleteFilmCommand(_context);    
            command.FilmId = film.Id;


            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var filmCheck = _context.Films.SingleOrDefault(filmCheck=> filmCheck.Id == film.Id);
            filmCheck.Should().BeNull();
        }
    }
}