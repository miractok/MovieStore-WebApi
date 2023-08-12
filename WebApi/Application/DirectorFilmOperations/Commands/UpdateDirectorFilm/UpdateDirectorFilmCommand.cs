using WebApi.DBOperations;

namespace WebApi.Application.DirectorFilmOperations.Commands.UpdateDirectorFilm
{
    public class UpdateDirectorFilmCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int DataId { get; set; }
        public UpdateDirectorFilmModel Model { get; set; }

        public UpdateDirectorFilmCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var directorFilm = _context.DirectorFilms.SingleOrDefault(x=> x.Id == DataId);
            if(directorFilm == null)
                throw new InvalidOperationException("Data could not be found!");

            var director = _context.Directors.SingleOrDefault(x => x.Id == Model.DirectorId);
            if(director == null)
                throw new InvalidOperationException("Director could not be found!");

            var film = _context.Films.SingleOrDefault(x => x.Id == Model.FilmId);
            if(film == null)
                throw new InvalidOperationException("Film could not be found!");

            directorFilm.FilmId = Model.FilmId != default ? Model.FilmId : directorFilm.FilmId;
            directorFilm.DirectorId = Model.DirectorId != default ? Model.DirectorId : directorFilm.DirectorId;
            directorFilm.IsActive = Model.IsActive;

            _context.DirectorFilms.Update(directorFilm);
            _context.SaveChanges();
        }

    }

    public class UpdateDirectorFilmModel
    {
        public int FilmId { get; set; }
        public int DirectorId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}