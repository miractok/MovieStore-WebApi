using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorFilmOperations.Queries.GetDirectorFilm
{
    public class GetDirectorFilmQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetDirectorFilmQuery(IMapper mapper, IMovieStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<DirectorFilmViewModel> Handle()
        {
            var directorFilmList = _context.DirectorFilms.Include(x => x.Film).Include(x => x.Director).OrderBy(x=> x.Id).ToList<DirectorFilm>();
            List<DirectorFilmViewModel> vm = _mapper.Map<List<DirectorFilmViewModel>>(directorFilmList);

            return vm;
        }

    }

    public class DirectorFilmViewModel
    {
        public int FilmId { get; set; }
        public int DirectorId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}