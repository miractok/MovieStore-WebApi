using WebApi.DBOperations;

namespace WebApi.Application.ActorFilmOperations.Commands.UpdateActorFilm
{
    public class UpdateActorFilmCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int DataId { get; set; }
        public UpdateActorFilmModel Model { get; set; }

        public UpdateActorFilmCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var actorFilm = _context.ActorFilms.SingleOrDefault(x=> x.Id == DataId);
            if(actorFilm == null)
                throw new InvalidOperationException("Data could not be found!");

            var actor = _context.Actors.SingleOrDefault(x => x.Id == Model.ActorId);
            if(actor == null)
                throw new InvalidOperationException("Actor could not be found!");

            var film = _context.Films.SingleOrDefault(x => x.Id == Model.FilmId);
            if(film == null)
                throw new InvalidOperationException("Film could not be found!");

            actorFilm.FilmId = Model.FilmId != default ? Model.FilmId : actorFilm.FilmId;
            actorFilm.ActorId = Model.ActorId != default ? Model.ActorId : actorFilm.ActorId;
            actorFilm.IsActive = Model.IsActive;

            _context.ActorFilms.Update(actorFilm);
            _context.SaveChanges();
        }

    }

    public class UpdateActorFilmModel
    {
        public int FilmId { get; set; }
        public int ActorId { get; set; }
        public bool IsActive { get; set; }
    }
}