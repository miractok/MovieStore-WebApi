using WebApi.DBOperations;

namespace WebApi.Application.ActorFilmOperations.Commands.DeleteActorFilm
{
    public class DeleteActorFilmCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int DataId { get; set; }

        public DeleteActorFilmCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var actorFilm = _context.ActorFilms.SingleOrDefault(x=> x.Id == DataId);
            if(actorFilm == null)
                throw new InvalidOperationException("Data could not be found!");

            _context.ActorFilms.Remove(actorFilm);
            _context.SaveChanges();
        }
    }
}