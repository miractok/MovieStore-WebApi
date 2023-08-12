using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.FilmOperations.Queries.GetFilmDetails
{
    public class GetFilmDetailsQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int FilmId { get; set; }
        public GetFilmDetailsQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public FilmsViewIdModel Handle()
        {
            var film = _context.Films.Include(x=> x.ActorFilm.Where(e => e.IsActive)).ThenInclude(x=> x.Actor).Include(x=> x.Genre).Include(x=> x.DirectorFilm.Where(e => e.IsActive)).ThenInclude(x => x.Director).Where(film=> film.Id == FilmId).SingleOrDefault();
            if(film == null)
                throw new InvalidOperationException("The Id you entered does not match any film.");
            FilmsViewIdModel vm = _mapper.Map<FilmsViewIdModel>(film); 
            return vm;
        }
    }

    public class FilmsViewIdModel
    {
        public string? Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string? Genre { get; set; }
        public IReadOnlyCollection<string>? Director { get; set; }
        public IReadOnlyCollection<string>? Actors { get; set; }
        public double Price { get; set; }
    }
}