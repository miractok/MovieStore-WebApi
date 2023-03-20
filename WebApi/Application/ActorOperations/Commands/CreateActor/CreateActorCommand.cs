using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommand
    {
        public CreateActorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateActorCommand(IMapper mapper, IMovieStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Handle()
        {
            var actor = _context.Actors.SingleOrDefault(x => x.NameSurname == Model.NameSurname);
            if(actor != null)
                throw new InvalidOperationException("This Actor already exists.");

            actor = _mapper.Map<Actor>(Model);

            _context.Actors.Add(actor);
            _context.SaveChanges();
        }
    }

    public class CreateActorViewModel
    {
        public string NameSurname { get; set; }
    }
}