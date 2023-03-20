using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.FilmOperations.Queries.GetMovieDetails
{
    public class GetMovieDetailQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int FilmId { get; set; }
        public GetMovieDetailQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public FilmsViewIdModel Handle()
        {
            var film = _context.Films.Include(x=> x.ActorFilm).ThenInclude(x=> x.Actor).Include(x=> x.Genre).Include(x=> x.DirectorFilm).ThenInclude(x => x.Director).Where(film=> film.Id == FilmId).SingleOrDefault();
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