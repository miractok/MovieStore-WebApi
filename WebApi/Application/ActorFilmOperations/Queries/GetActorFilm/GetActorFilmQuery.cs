using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorFilmOperations.Queries.GetActorFilm
{
    public class GetActorFilmQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetActorFilmQuery(IMapper mapper, IMovieStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<ActorFilmViewModel> Handle()
        {
            var actorFilmList = _context.ActorFilms.Include(x => x.Film).Include(x => x.Actor).OrderBy(x=> x.Id).ToList<ActorFilm>();
            List<ActorFilmViewModel> vm = _mapper.Map<List<ActorFilmViewModel>>(actorFilmList);

            return vm;
        }

    }

    public class ActorFilmViewModel
    {
        public int FilmId { get; set; }
        public int ActorId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}