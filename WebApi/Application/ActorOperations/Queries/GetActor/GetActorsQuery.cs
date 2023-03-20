using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Queries.GetActor
{
    public class GetActorsQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetActorsQuery(IMapper mapper, IMovieStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<ActorViewModel> Handle()
        {
            var actorList = _context.Actors.Include(x=> x.ActorFilm).ThenInclude(x=> x.Film).OrderBy(x=> x.Id).ToList<Actor>();
            List<ActorViewModel> vm = _mapper.Map<List<ActorViewModel>>(actorList);

            return vm;
        }

    }

    public class ActorViewModel
    {
        public string NameSurname { get; set; }
        public virtual ICollection<string> Films { get; set; }
        public bool IsActive { get; set; } = true;
    }
}