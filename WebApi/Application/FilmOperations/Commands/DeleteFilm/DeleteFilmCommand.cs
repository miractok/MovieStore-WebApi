using WebApi.DBOperations;

namespace WebApi.Application.FilmOperations.Commands.DeleteFilm
{
    public class DeleteFilmCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int FilmId { get; set; }

        public DeleteFilmCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var film = _context.Films.SingleOrDefault(x=> x.Id == FilmId);
            if(film == null)
                throw new InvalidOperationException("Film could not be found!");

            _context.Films.Remove(film);
            _context.SaveChanges();
        }
    }
}