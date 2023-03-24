using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.FilmOperations.Commands.CreateFilm
{
    public class CreateFilmCommand
    {
        public CreateFilmModel? Model { get; set; }
        public IMovieStoreDbContext _context { get; set; }
        public IMapper _mapper { get; set; }

        public CreateFilmCommand(IMapper mapper, IMovieStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Handle()
        {
            var film = _context.Films.SingleOrDefault(x=> x.Title.ToLower().Trim() == Model.Title.ToLower().Trim());
            if(film != null)
                throw new InvalidOperationException("Film already exists!");
            film = _mapper.Map<Film>(Model);
  
            _context.Films.Add(film);
            _context.SaveChanges();
        }
    }

    public class CreateFilmModel
    {
        public string? Title { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreId { get; set; }
        public double Price { get; set; }
    }
}