using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.FavouriteGenreOperations.Commands.UpdateFavouriteGenre;
using WebApi.DBOperations;

namespace Applications.FavouriteGenreOperations.Commands.UpdateCommand
{
    public class UpdateFavouriteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateFavouriteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenDataIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            UpdateFavouriteGenreCommand command = new UpdateFavouriteGenreCommand(_context);
            command.DataId = 789;

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Data could not be found!");
        }

        [Fact]
        public void WhenWrongCustomerIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            UpdateFavouriteGenreModel model = new UpdateFavouriteGenreModel() { CustomerId = 782, GenreId = 1};

            //act
            UpdateFavouriteGenreCommand command = new UpdateFavouriteGenreCommand(_context);
            command.Model = model;
            command.DataId = 2;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Customer could not be found!");
        }

        [Fact]
        public void WhenWrongFavouriteGenreIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            UpdateFavouriteGenreModel model = new UpdateFavouriteGenreModel() {  CustomerId = 1, GenreId = 753};

            //act
            UpdateFavouriteGenreCommand command = new UpdateFavouriteGenreCommand(_context);
            command.Model = model;
            command.DataId = 3;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre could not be found!");
        }

        
        //Happy Case
        [Fact]
        public void WhenValidInputsAreGiven_FavouriteGenreRelation_ShouldBeUpdated()
        {
            //arrange
            UpdateFavouriteGenreCommand command = new UpdateFavouriteGenreCommand(_context);
            UpdateFavouriteGenreModel model = new UpdateFavouriteGenreModel() {CustomerId = 1, GenreId = 4};
            command.Model = model;
            command.DataId = 3;

            //act

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var updateFavouriteGenre = _context.FavouriteGenres.SingleOrDefault(actorfilm => actorfilm.CustomerId == model.CustomerId && actorfilm.GenreId == model.GenreId);
            updateFavouriteGenre.Should().NotBeNull();
        }
    }
}