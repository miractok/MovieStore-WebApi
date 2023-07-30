using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorFilmOperations.Commands.DeleteActorFilm;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.ActorFilmOperations.Commands.DeleteCommand
{
    public class DeleteActorFilmCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteActorFilmCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenDataIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            DeleteActorFilmCommand command = new DeleteActorFilmCommand(_context);

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
            var actorFilm = new ActorFilm() {ActorId = 2, FilmId = 4};
            _context.ActorFilms.Add(actorFilm);
            _context.SaveChanges();

            DeleteActorFilmCommand command = new DeleteActorFilmCommand(_context);    
            command.DataId = actorFilm.Id;


            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var dataCheck = _context.ActorFilms.SingleOrDefault(dataCheck=> dataCheck.Id == actorFilm.Id);
            dataCheck.Should().BeNull();
        }
    }
}