using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.FavouriteGenreOperations.Commands.CreateFavouriteGenre
{
    public class CreateFavouriteGenreCommand
    {
        public CreateFavouriteGenreViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateFavouriteGenreCommand(IMapper mapper, IMovieStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Handle()
        {
            var favouriteGenre = _context.FavouriteGenres.SingleOrDefault(x => x.CustomerId == Model.CustomerId && x.GenreId == Model.GenreId);

            
            if(favouriteGenre != null)
                throw new InvalidOperationException("This relation already exists.");

            var customer = _context.Customers.SingleOrDefault(x => x.Id == Model.CustomerId);
            if(customer == null)
                throw new InvalidOperationException("Customer could not be found!");

            var genre = _context.Genres.SingleOrDefault(x => x.Id == Model.GenreId);
            if(genre == null)
                throw new InvalidOperationException("Genre could not be found!");

            favouriteGenre = _mapper.Map<FavouriteGenre>(Model);

            _context.FavouriteGenres.Add(favouriteGenre);
            _context.SaveChanges();
        }
    }

    public class CreateFavouriteGenreViewModel
    {
        public int CustomerId { get; set; }
        public int GenreId { get; set; }
    }
}