using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorFilmOperations.Commands.CreateDirectorFilm
{
    public class CreateDirectorFilmCommand
    {
        public CreateDirectorFilmViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateDirectorFilmCommand(IMapper mapper, IMovieStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Handle()
        {
            var directorFilm = _context.DirectorFilms.SingleOrDefault(x => x.DirectorId == Model.DirectorId && x.FilmId == Model.FilmId);

            
            if(directorFilm != null)
                throw new InvalidOperationException("This relation already exists.");

            var director = _context.Directors.SingleOrDefault(x => x.Id == Model.DirectorId);
            if(director == null)
                throw new InvalidOperationException("Director could not be found!");

            var film = _context.Films.SingleOrDefault(x => x.Id == Model.FilmId);
            if(film == null)
                throw new InvalidOperationException("Film could not be found!");

            directorFilm = _mapper.Map<DirectorFilm>(Model);

            _context.DirectorFilms.Add(directorFilm);
            _context.SaveChanges();
        }
    }

    public class CreateDirectorFilmViewModel
    {
        public int FilmId { get; set; }
        public int DirectorId { get; set; }
    }
}