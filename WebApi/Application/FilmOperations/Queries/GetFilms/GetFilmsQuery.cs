using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.FilmOperations.Queries.GetFilms
{
    public class GetFilmsQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetFilmsQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<FilmViewModel> Handle()
        {
            var movieList = _context.Films.Include(x=> x.ActorFilm.Where(e => e.IsActive)).ThenInclude(x=> x.Actor).Include(x=> x.Genre).Include(x=> x.DirectorFilm.Where(e => e.IsActive)).ThenInclude(x => x.Director).OrderBy(x=> x.Id).ToList<Film>();
            List<FilmViewModel> vm = _mapper.Map<List<FilmViewModel>>(movieList);

            return vm;
        }
    }

    public class FilmViewModel
    {
        public string? Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string? Genre { get; set; }
        public IReadOnlyCollection<string>? Director { get; set; }
        public IReadOnlyCollection<string>? Actors { get; set; }
        public double Price { get; set; }
    }
}