using WebApi.DBOperations;

namespace WebApi.Application.DirectorFilmOperations.Commands.DeleteDirectorFilm
{
    public class DeleteDirectorFilmCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int DataId { get; set; }

        public DeleteDirectorFilmCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var directorFilm = _context.DirectorFilms.SingleOrDefault(x=> x.Id == DataId);
            if(directorFilm == null)
                throw new InvalidOperationException("Data could not be found!");

            _context.DirectorFilms.Remove(directorFilm);
            _context.SaveChanges();
        }
    }
}