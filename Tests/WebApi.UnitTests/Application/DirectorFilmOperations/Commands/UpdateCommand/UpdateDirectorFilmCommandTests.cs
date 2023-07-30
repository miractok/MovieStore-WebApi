using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorFilmOperations.Commands.UpdateDirectorFilm;
using WebApi.DBOperations;

namespace Applications.DirectorFilmOperations.Commands.UpdateCommand
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
            UpdateDirectorFilmCommand command = new UpdateDirectorFilmCommand(_context);
            command.DataId = 789;

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Data could not be found!");
        }

        [Fact]
        public void WhenWrongDirectorIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            UpdateDirectorFilmModel model = new UpdateDirectorFilmModel() { DirectorId = 782, FilmId = 1};

            //act
            UpdateDirectorFilmCommand command = new UpdateDirectorFilmCommand(_context);
            command.Model = model;
            command.DataId = 2;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Director could not be found!");
        }

        [Fact]
        public void WhenWrongFilmIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            UpdateDirectorFilmModel model = new UpdateDirectorFilmModel() { DirectorId = 1, FilmId = 246};

            //act
            UpdateDirectorFilmCommand command = new UpdateDirectorFilmCommand(_context);
            command.Model = model;
            command.DataId = 3;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film could not be found!");
        }

        
        //Happy Case
        [Fact]
        public void WhenValidInputsAreGiven_DirectorFilmRelation_ShouldBeUpdated()
        {
            //arrange
            UpdateDirectorFilmCommand command = new UpdateDirectorFilmCommand(_context);
            UpdateDirectorFilmModel model = new UpdateDirectorFilmModel() {DirectorId = 2, FilmId = 4};
            command.Model = model;
            command.DataId = 3;

            //act

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var updatedirector = _context.DirectorFilms.SingleOrDefault(directorfilm => directorfilm.DirectorId == model.DirectorId && directorfilm.FilmId == model.FilmId);
            updatedirector.Should().NotBeNull();
        }
    }
}