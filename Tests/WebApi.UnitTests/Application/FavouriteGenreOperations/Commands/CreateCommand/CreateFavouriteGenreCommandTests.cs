using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.FavouriteGenreOperations.Commands.CreateFavouriteGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.FavouriteGenreOperations.Commands.CreateCommand
{
    public class CreateFavouriteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateFavouriteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongCustomerIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            CreateFavouriteGenreViewModel model = new CreateFavouriteGenreViewModel() { CustomerId = 565, GenreId = 1};

            //act
            CreateFavouriteGenreCommand command = new CreateFavouriteGenreCommand(_mapper,_context);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Customer could not be found!");
        }

        [Fact]
        public void WhenWrongGenreIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            CreateFavouriteGenreViewModel model = new CreateFavouriteGenreViewModel() { CustomerId = 1, GenreId = 465};

            //act
            CreateFavouriteGenreCommand command = new CreateFavouriteGenreCommand(_mapper,_context);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre could not be found!");
        }

        [Fact]
        public void WhenAlreadyExistCustomerAndGenreIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            var favouriteGenre = new FavouriteGenre() {CustomerId = 3 , GenreId = 4};
            _context.FavouriteGenres.Add(favouriteGenre);
            _context.SaveChanges();

            CreateFavouriteGenreViewModel model = new CreateFavouriteGenreViewModel() { CustomerId = 3, GenreId = 4};

            //act
            CreateFavouriteGenreCommand command = new CreateFavouriteGenreCommand(_mapper,_context);
            command.Model = model;
            
            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("This relation already exists.");
        }

        [Fact]
        public void WhenNotExistGenerAndCustomerIdIsGiven_ActorFilmRelation_ShouldBeCreated()
        {
            // arrange
            CreateFavouriteGenreViewModel model = new CreateFavouriteGenreViewModel() { CustomerId = 4, GenreId = 4};
            CreateFavouriteGenreCommand command = new CreateFavouriteGenreCommand(_mapper,_context);
            command.Model = model;
        
            // act
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var favouriteGenre = _context.FavouriteGenres.SingleOrDefault(s => s.CustomerId == model.CustomerId && s.GenreId == model.GenreId);
            
            favouriteGenre.Should().NotBeNull();
        }
    }
}