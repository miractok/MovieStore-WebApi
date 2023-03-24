using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorFilmOperations.Commands.CreateActorFilm
{
    public class CreateActorFilmCommand
    {
        public CreateActorFilmViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateActorFilmCommand(IMapper mapper, IMovieStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Handle()
        {
            var actorFilm = _context.ActorFilms.SingleOrDefault(x => x.ActorId == Model.ActorId && x.FilmId == Model.FilmId);
            if(actorFilm != null)
                throw new InvalidOperationException("This relation already exists.");

            var actor = _context.Actors.SingleOrDefault(x => x.Id == Model.ActorId);
            if(actor == null)
                throw new InvalidOperationException("Actor could not be found!");

            var film = _context.Films.SingleOrDefault(x => x.Id == Model.FilmId);
            if(film == null)
                throw new InvalidOperationException("Film could not be found!");

            actorFilm = _mapper.Map<ActorFilm>(Model);

            _context.ActorFilms.Add(actorFilm);
            _context.SaveChanges();
        }
    }

    public class CreateActorFilmViewModel
    {
        public int FilmId { get; set; }
        public int ActorId { get; set; }
    }
}