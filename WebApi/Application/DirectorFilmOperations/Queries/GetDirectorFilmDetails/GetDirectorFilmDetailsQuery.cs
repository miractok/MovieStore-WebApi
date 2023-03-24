using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.DirectorFilmOperations.Queries.GetDirectorFilmDetails
{
    public class GetDirectorFilmDetailQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int DirectorFilmId { get; set; }
        public GetDirectorFilmDetailQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DirectorFilmViewIdModel Handle()
        {
            var directorFilm = _context.DirectorFilms.Include(x=> x.Director).Include(x=> x.Film).Where(directorFilm=> directorFilm.Id == DirectorFilmId).SingleOrDefault();

            if(directorFilm == null)
                throw new InvalidOperationException("The Id you entered does not match any DirectorFilm relation.");

            DirectorFilmViewIdModel vm = _mapper.Map<DirectorFilmViewIdModel>(directorFilm); 
            
            return vm;
        }
    }

    public class DirectorFilmViewIdModel
    {
        public int FilmId { get; set; }
        public int DirectorId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}