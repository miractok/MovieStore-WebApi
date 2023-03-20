using WebApi.DBOperations;

namespace WebApi.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int ActorId { get; set; }
        public UpdateActorModel Model { get; set; }

        public UpdateActorCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var actor = _context.Actors.SingleOrDefault(x=> x.Id == ActorId);
            if(actor == null)
                throw new InvalidOperationException("Actor could not be found!");

            if(_context.Actors.Any(x=> x.NameSurname.ToLower() == Model.NameSurname.ToLower() && x.Id != ActorId))
                throw new InvalidOperationException("Actor already exists!");

            actor.NameSurname = Model.NameSurname != default ? Model.NameSurname : actor.NameSurname;

            _context.Actors.Update(actor);
            _context.SaveChanges();
        }

    }

    public class UpdateActorModel
    {
        public string NameSurname { get; set; }
    }
}