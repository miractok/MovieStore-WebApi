using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorFilmOperations.Commands.DeleteDirectorFilm;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.DirectorFilmOperations.Commands.DeleteCommand
{
    public class DeleteDirectorFilmCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteDirectorFilmCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenDataIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            DeleteDirectorFilmCommand command = new DeleteDirectorFilmCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Data could not be found!");
        }
        //Happy Case
        [Fact]
        public void WhenDataIdIsValid_Film_ShouldBeDeleted()
        {
            var directorFilm = new DirectorFilm() {DirectorId = 2, FilmId = 4};
            _context.DirectorFilms.Add(directorFilm);
            _context.SaveChanges();

            DeleteDirectorFilmCommand command = new DeleteDirectorFilmCommand(_context);    
            command.DataId = directorFilm.Id;


            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var dataCheck = _context.DirectorFilms.SingleOrDefault(dataCheck=> dataCheck.Id == directorFilm.Id);
            dataCheck.Should().BeNull();
        }
    }
}