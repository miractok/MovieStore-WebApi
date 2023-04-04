using WebApi.DBOperations;

namespace WebApi.Application.FavouriteGenreOperations.Commands.DeleteFavouriteGenre
{
    public class DeleteFavouriteGenreCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int DataId { get; set; }

        public DeleteFavouriteGenreCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var favouriteGenre = _context.FavouriteGenres.SingleOrDefault(x=> x.Id == DataId);
            if(favouriteGenre == null)
                throw new InvalidOperationException("Data could not be found!");

            _context.FavouriteGenres.Remove(favouriteGenre);
            _context.SaveChanges();
        }
    }
}