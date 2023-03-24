using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.ActorFilmOperations.Queries.GetActorFilmDetails
{
    public class GetActorFilmDetailQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int ActorFilmId { get; set; }
        public GetActorFilmDetailQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ActorFilmViewIdModel Handle()
        {
            var actorFilm = _context.ActorFilms.Include(x=> x.Actor).Include(x=> x.Film).Where(actorFilm=> actorFilm.Id == ActorFilmId).SingleOrDefault();

            if(actorFilm == null)
                throw new InvalidOperationException("The Id you entered does not match any actorFilm relation.");

            ActorFilmViewIdModel vm = _mapper.Map<ActorFilmViewIdModel>(actorFilm); 
            
            return vm;
        }
    }

    public class ActorFilmViewIdModel
    {
        public int FilmId { get; set; }
        public int ActorId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}