using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.ActorOperations.Queries.GetActorDetails
{
    public class GetActorDetailQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int ActorId { get; set; }
        public GetActorDetailQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ActorsViewIdModel Handle()
        {
            var actor = _context.Actors.Include(x=> x.ActorFilm).ThenInclude(x=> x.Film).Where(actor=> actor.Id == ActorId).SingleOrDefault();

            if(actor == null)
                throw new InvalidOperationException("The Id you entered does not match any actor.");

            ActorsViewIdModel vm = _mapper.Map<ActorsViewIdModel>(actor); 
            
            return vm;
        }
    }

    public class ActorsViewIdModel
    {
        public string NameSurname { get; set; }
        public virtual ICollection<string> Films { get; set; }
        public bool IsActive { get; set; } = true;
    }
}