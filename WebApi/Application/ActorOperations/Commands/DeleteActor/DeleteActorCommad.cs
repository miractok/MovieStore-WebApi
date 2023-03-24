using WebApi.DBOperations;

namespace WebApi.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int ActorId { get; set; }

        public DeleteActorCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var actor = _context.Actors.SingleOrDefault(x=> x.Id == ActorId);
            if(actor == null)
                throw new InvalidOperationException("Actor could not be found!");

            var actorCheck = _context.ActorFilms.Where(x=> x.ActorId == actor.Id).Any();
            if(actorCheck)
                throw new InvalidOperationException("Actor cannot be deleted! First remove Actor/Actress from the Film.");

            _context.Actors.Remove(actor);
            _context.SaveChanges();
        }
    }
}