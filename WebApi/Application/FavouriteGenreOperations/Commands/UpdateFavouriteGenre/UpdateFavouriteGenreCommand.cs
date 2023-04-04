using WebApi.DBOperations;

namespace WebApi.Application.FavouriteGenreOperations.Commands.UpdateFavouriteGenre
{
    public class UpdateFavouriteGenreCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int DataId { get; set; }
        public UpdateFavouriteGenreModel Model { get; set; }

        public UpdateFavouriteGenreCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var favouriteGenre = _context.FavouriteGenres.SingleOrDefault(x=> x.Id == DataId);
            if(favouriteGenre == null)
                throw new InvalidOperationException("Data could not be found!");

            var customer = _context.Customers.SingleOrDefault(x => x.Id == Model.CustomerId);
            if(customer == null)
                throw new InvalidOperationException("Customer could not be found!");

            var genre = _context.Genres.SingleOrDefault(x => x.Id == Model.GenreId);
            if(genre == null)
                throw new InvalidOperationException("Genre could not be found!");

            favouriteGenre.CustomerId = Model.CustomerId != default ? Model.CustomerId : favouriteGenre.CustomerId;
            favouriteGenre.GenreId = Model.GenreId != default ? Model.GenreId : favouriteGenre.GenreId;

            _context.FavouriteGenres.Update(favouriteGenre);
            _context.SaveChanges();
        }

    }

    public class UpdateFavouriteGenreModel
    {
        public int CustomerId { get; set; }
        public int GenreId { get; set; }
        public bool IsActive { get; set; }
    }
}